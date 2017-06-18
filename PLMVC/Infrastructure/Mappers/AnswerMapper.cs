using BLL.Interface.Entities;
using PLMVC.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class AnswerMapper
    {
        public static BllAnswer ToBllAnswer(this AnswerViewModel mvcAnswer)
        {
            if (mvcAnswer == null)
                throw new ArgumentNullException(nameof(mvcAnswer));
            var bllAnswer = new BllAnswer()
            {
                Id = mvcAnswer.Id,
                Text = mvcAnswer.Text,
                IsRight = mvcAnswer.IsRight,
                QuestionId = mvcAnswer.QuestionId
            };
            return bllAnswer;
        }

        public static AnswerViewModel ToMvcAnswer(this BllAnswer bllAnswer)
        {
            if (bllAnswer == null)
                throw new ArgumentNullException(nameof(bllAnswer));
            var mvcAnswer = new AnswerViewModel()
            {
                Id = bllAnswer.Id,
                Text = bllAnswer.Text,
                IsRight = bllAnswer.IsRight,
                QuestionId = bllAnswer.QuestionId
            };
            return mvcAnswer;
        }
    }
}