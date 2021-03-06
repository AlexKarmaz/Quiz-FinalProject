﻿using BLL.Interface.Interfaces;
using PLMVC.Models.Test;
using PLMVC.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLMVC.Models.Question;
using BLL.Interface.Entities;
using System.Net;
using System.Diagnostics;
using PLMVC.Models.Theme;
using PLMVC.Models.TestResult;
using System.Xml.Linq;

namespace PLMVC.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ITestResultService testResultService;
        private readonly IUserService userService;
        private readonly IThemeService themeService;
        private readonly IProfileService profileService;

        public TestController(ITestService testService, ITestResultService testResultService, IUserService userService, IThemeService themeService, IProfileService profileService)
        {
            this.testService = testService;
            this.testResultService = testResultService;
            this.userService = userService;
            this.themeService = themeService;
            this.profileService = profileService;
        }

        [HttpGet]
        public ActionResult CreateTest()
        {
            SelectList themes = new SelectList(themeService.GetAll(),"Id", "Name");
           ViewBag.Themes = themes;
            if (Request.IsAjaxRequest())
                return PartialView("_CreateTest");
           return View("_CreateTest");
        }

        [HttpPost]
        public ActionResult CreateTest(CreateTestViewModel model)
        {
            if (testService.GetOneByPredicate(u => u.Title.ToLower() == model.Title.ToLower()) != null)
            {
                ModelState.AddModelError(String.Empty, "Test with this title already registered.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                int userId = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;
                model.Questions = new List<QuestionViewModel>();
                var test = model.ToBllTest();
                test.DateCreation = DateTime.Now;
                test.UserId = userId;
                test.TestResults = new List<BllTestResult>();
                testService.Create(test);
                if (Request.IsAjaxRequest())
                {
                 int testId = testService.GetOneByPredicate(t => t.Title == model.Title).Id;
                 return RedirectToAction("TestDetails","Test", new { testId = testId }); 
                }
                return Redirect(Url.Action("ShowLastTests", "Test"));
            }
            return PartialView("_CreateTest");
        }

        [HttpGet]
        public ActionResult TestDetails(int? testId)
        {
            string userName;
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bllTest = testService.GetOneByPredicate(t=>t.Id == testId);
            bool isUserExist = userService.IsExistUser(bllTest.UserId);
            if(isUserExist)
            {
                userName = userService.GetOneByPredicate(u => u.Id == bllTest.UserId).UserName;
            }
            else
            {
                userName = "Creator was deleted";
            }
          
            string themeName = themeService.GetOneByPredicate(t => t.Id == bllTest.ThemeId).Name;
            var mvcTest = bllTest.ToMvcTest();
            mvcTest.UserName = userName;
            mvcTest.ThemeName = themeName;
            ViewBag.TestId = bllTest.Id;
            if (Request.IsAjaxRequest())
                return PartialView("_TestDetails",mvcTest);
            return View("_TestDetails",mvcTest);
        }

        [HttpGet]
        public ActionResult ShowLastTests()
        {
            var tests = testService.GetAll().Reverse().Take(10);
            var mvcTests = tests.Select(t => t.ToMvcAllTests());
            ViewBag.Categories = themeService.GetAll();
            if (Request.IsAjaxRequest())
                return PartialView("_ShowLastTests", mvcTests);
            return View("_ShowLastTests", mvcTests);
        }

        [HttpGet]
        public ActionResult ShowTestsByTheme(int? categoryId)
        {
            var tests = testService.GetAllByPredicate(t => t.ThemeId == categoryId);
            if (tests == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var mvcTests = tests.Select(t => t.ToMvcAllTests());
            ViewBag.ThemeName = themeService.GetById(Convert.ToInt32(categoryId)).Name;
            if (Request.IsAjaxRequest())
                return PartialView("_ShowTestsByTheme", mvcTests);
            return View("_ShowTestsByTheme", mvcTests);
        }

        [HttpGet]
        public ActionResult ShowCreateTests(int? profileId)
        {
            if (profileId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var tests = profileService.GetOneByPredicate(p => p.Id == profileId).CreatedTests;
            var mvcTests = tests.Select(t => t.ToMvcProfileCreateTest()).ToArray();
            for (int i = 0; i < mvcTests.Length; i++)
            {
                mvcTests[i].ThemeName = themeService.GetById(mvcTests[i].ThemeId).Name;
            }

            if (Request.IsAjaxRequest())
                return PartialView("_ShowCreateTests", mvcTests);
            return View("_ShowCreateTests", mvcTests);
        }

        [HttpGet]
        public ActionResult ShowPassedTests(int? profileId)
        {
            if (profileId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            int userId = profileService.GetById(Convert.ToInt32(profileId)).UserId;
            var testResults = testResultService.GetAllByPredicate(r => r.UserId == userId);
            var mvcTestResults = testResults.Select(t => t.ToMvcPassedTestResult()).ToArray();
            for(int i = 0; i < mvcTestResults.Length; i++)
            {
                mvcTestResults[i].Title = testService.GetById(mvcTestResults[i].TestId).Title;
            }

            if (Request.IsAjaxRequest())
                return PartialView("_ShowPassedTests", mvcTestResults);
            return View("_ShowPassedTests", mvcTestResults);
        }

        [HttpGet]
        public ActionResult EditTest(int? testId)
        {
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var test = testService.GetOneByPredicate(t => t.Id == testId);
            var mvcTest = test.ToMvcEditTest();
            SelectList themes = new SelectList(themeService.GetAll(), "Id", "Name");
            ViewBag.TestId = testId;
            ViewBag.Themes = themes;
            if (Request.IsAjaxRequest())
                return PartialView("_EditTest", mvcTest);
            return View("_EditTest", mvcTest);
        }

        [HttpPost]
        public ActionResult EditTest(EditTestViewModel model, int? testId)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var bllTest = model.ToBllEditTest();
                bllTest.Id = Convert.ToInt32(testId);
                testService.Update(bllTest);
                return RedirectToAction("TestDetails", "Test", new { testId = testId });
            }
            return RedirectToAction("EditTest", "Test", new { testId = testId });
        }

        
        public void DeleteTest(int testId)
        {
            var test = testService.GetOneByPredicate(t => t.Id == testId);
            testService.Delete(test);
        }

        [HttpGet]
        public ActionResult DeleteTests(int testId)
        {
            DeleteTest(testId);
            return RedirectToAction("ShowLastTests");
        }

        [HttpGet]
        public ActionResult DeleteTestFromProfile(int testId)
        {
            DeleteTest(testId);
            int userId = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;
            var profileId = profileService.GetOneByPredicate(p => p.UserId == userId).Id;
            return RedirectToAction("ShowCreateTests", "Test", new { profileId = profileId });
        }

        [HttpGet]
        public ActionResult SearchTest(string searchString)
        {
            if (searchString == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            searchString = searchString.ToLower();
            var tests = testService.Search(searchString);
            var mvcTests = tests.Select(t => t.ToMvcAllTests());

            if (Request.IsAjaxRequest())
                return PartialView("_Search", mvcTests);
            return View("_Search", mvcTests);
        }

        [HttpGet]
        public ActionResult Preview(int testId)
        {
            bool isReady = testService.IsTestReady(testId);
            var test = testService.GetById(testId);
            var mvcTest = test.ToMvcPreviewTest();

            if (Request.IsAjaxRequest())
            {
                if (isReady)
                    return PartialView("_Preview", mvcTest);
                return PartialView("_NotReadyTest");
            }

            if (isReady)
                return View("_Preview", mvcTest);
            return View("_NotReadyTest");
        }

        [HttpGet]
        public ActionResult PassingTest(int? testId)
        {
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var test = testService.GetById(Convert.ToInt32(testId));
            var passingTest = test.ToMvcPassingTest();
            passingTest.StartTest = DateTime.UtcNow;
            passingTest.Results = new bool[passingTest.Questions.Count][];
            for(int i = 0; i < passingTest.Questions.Count; i++)
            {
                passingTest.Results[i] = new bool[passingTest.Questions[i].Answers.Count];
            }
            if (Request.IsAjaxRequest())
               return PartialView("_PassingTest", passingTest);
            return View("_PassingTest", passingTest);
        }

        [HttpPost]
        public ActionResult PassingTest(PassingTestViewModel model)
        {
            bool isSuccess = false;
            var test = testService.GetById(model.Id);
            int userId = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;
            var testResults = testService.CheckAnswers(model.Id, model.Results);
            var runtime = (DateTime.UtcNow - model.StartTest);

            if (testResultService.CheckPercentAnswers(testResults, test.MinToSuccess) && testResultService.CheckTime(test.TimeLimit, runtime))
                isSuccess = true;
            var bllTestResult = new BllTestResult
            {
                TestId = model.Id,
                UserId = userId,
                Runtime = runtime,
                DateComplete = DateTime.Now,
                IsSuccess = isSuccess,
                Results = testResults.ToList()
            };
            testResultService.Create(bllTestResult);

            var mvcTestResult = bllTestResult.ToMvcStatistics();
            mvcTestResult.TimeLimit = test.TimeLimit;
            mvcTestResult.MinToSuccess = test.MinToSuccess;
            mvcTestResult.PercentCorrectAnswers = testResultService.GetPercentGoodAnswers(testResults);
            if (Request.IsAjaxRequest())
                return PartialView("_Statistics", mvcTestResult);
            return View("_Statistics", mvcTestResult);
        }

        [HttpGet]
        public ActionResult DeleteTestResult(int testResultId)
        {
            var testResult = testResultService.GetById(testResultId);
            int userId = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;
            var profileId = profileService.GetOneByPredicate(p=>p.UserId == userId).Id;
            testResultService.Delete(testResult);
            return RedirectToAction("ShowPassedTests", "Test",new { profileId = profileId });
        }

        [HttpGet]
        public ActionResult CreateTheme()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_CreateTheme");
            return View("_CreateTheme");
        }

        [HttpPost]
        public ActionResult CreateTheme(ThemeViewModel model)
        {
            if (themeService.GetOneByPredicate(t => t.Name == model.Name) != null)
            {
                ModelState.AddModelError("", "Category with this name already registered.");
                return View(model);
            }
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var newTheme = model.ToBllTheme();
            themeService.Create(newTheme);

            return RedirectToAction("CreateTest");
        }

        [HttpPost]
        public ActionResult ToXmlFile(int? userId)
        {
            if (userId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var testResults = testResultService.GetAllByPredicate(r => r.UserId == userId);
            var mvcTestResults = testResults.Select(t => t.ToMvcPassedTestResult()).ToArray();
            for (int i = 0; i < mvcTestResults.Length; i++)
            {
                mvcTestResults[i].Title = testService.GetById(mvcTestResults[i].TestId).Title;
            }

            List<PassedTestResult> tests = mvcTestResults.ToList();

            XDocument document = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XComment("It's your results")
            );

            XElement resultsElement = new XElement("Results");
            foreach (var result in tests)
            {
                XElement newElement = new XElement("Result");
                newElement.Add(new XAttribute("id", result.Id));
                newElement.Add(new XElement("Title", result.Title));
                newElement.Add(new XElement("TestId", result.TestId));
                newElement.Add(new XElement("UserId", result.UserId));
                newElement.Add(new XElement("Runtime", result.Runtime));
                newElement.Add(new XElement("DateOfPassing", result.DateComplete));
                newElement.Add(new XElement("Success", result.IsSuccess));

                resultsElement.Add(newElement);
            }

            document.Add(resultsElement);
            document.Save("F:\\Quiz-FinalProject\\Results.xml");

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}