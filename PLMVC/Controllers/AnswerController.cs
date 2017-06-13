﻿using BLL.Interface.Interfaces;
using PLMVC.Infrastructure.Mappers;
using PLMVC.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly ITestService testService;
        private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;


        public AnswerController(ITestService testService, IQuestionService questionService, IAnswerService answerService)
        {
            this.testService = testService;
            this.questionService = questionService;
            this.answerService = answerService;
        }

        [HttpGet]
        public ActionResult CreateQuestionAnswer(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // var test = testService.GetOneByPredicate(t => t.Id == testId);
            //var question = questionService.GetOneByPredicate(q => q.Id == questionId);
            //ViewBag.themeId = question.ThemeId;
            ViewBag.QuestionId = questionId;
            if (Request.IsAjaxRequest())
                return PartialView("_CreateQuestionAnswer");
            return View("_CreateQuestionAnswer");
        }

        [HttpPost]
        public ActionResult CreateQuestionAnswer(AnswerViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var answer = model.ToBllAnswer();
            answerService.Create(answer);

            return RedirectToAction("DetailsQuestionAnswers", "Answer", new { questionId = model.QuestionId });
        }

        [HttpGet]
        public ActionResult DetailsQuestionAnswers(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var answers = questionService.GetOneByPredicate(a => a.Id == questionId).Answers;
            var mvcAnswers = answers.Select(a => a.ToMvcAnswer());
            ViewBag.questionId = questionId;

            if (Request.IsAjaxRequest())
                return PartialView("_DetailsQuestionAnswers", mvcAnswers);
            return View("_DetailsQuestionAnswers", mvcAnswers);
        }
    }
}