using BLL.Interface.Interfaces;
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
    [Authorize]
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

        [HttpGet]
        public ActionResult DeleteQuestionAnswer(int? answerId)
        {
            if (answerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var answer = answerService.GetOneByPredicate(a => a.Id == answerId);
            if (answer == null)
            {
                return HttpNotFound();
            }
            answerService.Delete(answer);
            return RedirectToAction("DetailsQuestionAnswers", "Answer", new { questionId = answer.QuestionId });
        }

        [HttpGet]
        public ActionResult EditQuestionAnswer(int? answerId)
        {
            if (answerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var answer = answerService.GetOneByPredicate(a => a.Id == answerId);
            var mvcAnswer = answer.ToMvcAnswer();
            if (Request.IsAjaxRequest())
                return PartialView("_EditQuestionAnswer", mvcAnswer);
            return View("_EditQuestionAnswer", mvcAnswer);
        }

        [HttpPost]
        public ActionResult EditQuestionAnswer(AnswerViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var bllAnswer = model.ToBllAnswer();
                answerService.Update(bllAnswer);
            }
            return RedirectToAction("DetailsQuestionAnswers", "Answer", new { questionId = model.QuestionId });
        }
    }
}