using BLL.Interface.Interfaces;
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

namespace PLMVC.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly ITestResultService testResultService;
        private readonly IUserService userService;
        private readonly IThemeService themeService;

        public TestController(ITestService testService, ITestResultService testResultService, IUserService userService, IThemeService themeService)
        {
            this.testService = testService;
            this.testResultService = testResultService;
            this.userService = userService;
            this.themeService = themeService;
        }

        [HttpGet]
        public ActionResult CreateTest()
        {
            //System.Threading.Thread.Sleep(5000);
            SelectList themes = new SelectList(themeService.GetAll(),"Id", "Name");
           ViewBag.Themes = themes;
            if (Request.IsAjaxRequest())
                return PartialView("_CreateTest");
           return View("_CreateTest");
        }

        [HttpPost]
        public ActionResult CreateTest(CreateTestViewModel model)
        {
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
            try
            {
                userName = userService.GetOneByPredicate(u => u.Id == bllTest.UserId).UserName;
            }
            catch(Exception ex)
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
        public ActionResult DeleteTestFromAllUsers(int testId)
        {
            DeleteTest(testId);
            return RedirectToAction("ShowLastTests");
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
            
            var test = testService.GetById(testId);
            var mvcTest = test.ToMvcPreviewTest();
            //ViewBag.TimeLimit = test.TimeLimit;
            //ViewBag.MinToSuccess = test.MinToSuccess;
            //ViewBag.Title = test.Title;
            if (Request.IsAjaxRequest())
                return PartialView("_Preview", mvcTest);
            return View("_Preview", mvcTest);
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
            passingTest.StartTest = DateTime.Now;
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
            model.FinishTest = DateTime.Now;
            //var test = testService.GetById(testId);
            //var passingTest = test.ToMvcPassingTest();

            return RedirectToAction("Preview", new { testId = model.Id });
        }

    }
}