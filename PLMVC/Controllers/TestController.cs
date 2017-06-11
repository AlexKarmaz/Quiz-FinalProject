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
                model.ThemeId = 1;// ВЫБИРАТЬ НА ВЬЮХЕ 
                model.Questions = new List<QuestionViewModel>();
                var test = model.ToBllTest();
                test.DateCreation = DateTime.Now;
                test.UserId = userId;
                test.TestResults = new List<BllTestResult>();
                testService.Create(test);
                if (Request.IsAjaxRequest())
                {
                  return Redirect(Url.Action("Index", "Home"));// ССЫЛКА НА EDIT TEST PARTIAL VIEW 
                }
                return Redirect(Url.Action("Index", "Home"));
            }
            return PartialView("CreateTest");
        }
    }
}