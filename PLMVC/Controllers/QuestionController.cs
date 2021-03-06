﻿using BLL.Interface.Interfaces;
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
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ITestService testService;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;


        public QuestionController(ITestService testService, IQuestionService questionService, IAnswerService answerService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.answerService = answerService;
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
            if (Request.IsAjaxRequest())
                return PartialView("_CreateTestQuestion");
            return View("_CreateTestQuestion");
        }

        [HttpPost]
        public ActionResult CreateTestQuestion(QuestionViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Answers = new List<AnswerViewModel>();
            var question = model.ToBllQuestion();
            questionService.CreateAndUpdateTestId(question,Convert.ToInt32(model.TestId));
 
            return RedirectToAction("DetailsTestQuestions", "Question",new { testId = model.TestId});
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
            return View("_DetailsTestQuestions",mvcQuestions);
        }

        [HttpGet]
        public ActionResult DetailsOneQuestion(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = questionService.GetOneByPredicate(t=> t.Id == questionId);
            var mvcQuestion = question.ToMvcQuestion();
            if (Request.IsAjaxRequest())
                return PartialView("_DetailsQuestion", mvcQuestion);
            return View("_DetailsQuestion", mvcQuestion);
        }

        [HttpGet]
        public ActionResult EditTestQuestion(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = questionService.GetOneByPredicate(t => t.Id == questionId);
            var mvcQuestion = question.ToMvcQuestion();
            if (Request.IsAjaxRequest())
                return PartialView("_EditTestQuestion", mvcQuestion);
            return View("_EditTestQuestion", mvcQuestion);
        }

        [HttpPost]
        public ActionResult EditTestQuestion(QuestionViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                model.Answers = new List<AnswerViewModel>();
                var bllQuestion = model.ToBllQuestion();
                questionService.Update(bllQuestion);
            }
            return RedirectToAction("DetailsTestQuestions", "Question", new { testId = model.TestId });
        }

        [HttpGet]
        public ActionResult DeleteTestQuestion(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = questionService.GetOneByPredicate(q => q.Id == questionId);
            if (question == null)
            {
                return HttpNotFound();
            }
            questionService.Delete(question);
            return RedirectToAction("DetailsTestQuestions", "Question", new { testId = question.TestId });
        }
    }
}