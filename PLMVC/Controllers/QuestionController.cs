using BLL.Interface.Interfaces;
using PLMVC.Infrastructure.Mappers;
using PLMVC.Models.Answer;
using PLMVC.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ITestService testService;
        private readonly IQuestionService questionService;
    

        public QuestionController(ITestService testService, IQuestionService questionService)
        {
            this.testService = testService;
            this.questionService = questionService;
        }

        // GET: Question
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateTestQuestion(int? testId)
        {
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var test = testService.GetOneByPredicate(t => t.Id == testId);
            ViewBag.themeId = test.ThemeId;
            ViewBag.testId = testId;
            if (Request.IsAjaxRequest())
                return PartialView("_CreateTestQuestion");
            return View("_CreateTestQuestion");
        }

        [HttpPost]
        public ActionResult CreateTestQuestion(QuestionViewModel model,int? testId)
        {
            if (testId == null || model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //model.ThemeId = Convert.ToInt32(themeId);
            model.Answers = new List<AnswerViewModel>();
            var question = model.ToBllQuestion();
            questionService.CreateAndUpdateTestId(question,Convert.ToInt32(testId));
            //if (Request.IsAjaxRequest())
            //    return PartialView("_CreateTestQuestion", mvcTest);
            //return View("_CreateTestQuestion");
            return RedirectToAction("DetailsTestQuestions", "Question",new { testId = testId});
        }

        [HttpGet]
        public ActionResult DetailsTestQuestions(int? testId)
        {
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questions = testService.GetOneByPredicate(t => t.Id == testId).Questions;
            var mvcQuestions = questions.Select(q => q.ToMvcQuestion());
            ViewBag.testId = testId;
            if (Request.IsAjaxRequest())
                return PartialView("_DetailsTestQuestions",mvcQuestions);
            return View("_DetailsTestQuestions",questions);
        }

        [HttpGet]
        public ActionResult EditTestQuestion(int? testId)
        {
            if (testId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var questions = testService.GetOneByPredicate(t => t.Id == testId).Questions;
            var mvcQuestions = questions.Select(q => q.ToMvcQuestion());
            ViewBag.testId = testId;
            if (Request.IsAjaxRequest())
                return PartialView("_DetailsTestQuestions", mvcQuestions);
            return View("_DetailsTestQuestions", questions);
        }
    }
}