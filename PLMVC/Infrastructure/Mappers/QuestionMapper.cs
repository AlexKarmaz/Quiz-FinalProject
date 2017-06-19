using BLL.Interface.Entities;
using PLMVC.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class QuestionMapper
    {
        public static BllQuestion ToBllQuestion(this QuestionViewModel mvcQuestion)
        {
            if (mvcQuestion == null)
                return null;
            var bllQuestion = new BllQuestion()
            {
                Id = mvcQuestion.Id,
                ThemeId = mvcQuestion.ThemeId,
                Text = mvcQuestion.Text,
                TestId = mvcQuestion.TestId,
                Answers = mvcQuestion.Answers.Select(r => r.ToBllAnswer()).ToList()
            };
            return bllQuestion;
        }

        public static QuestionViewModel ToMvcQuestion(this BllQuestion bllQuestion)
        {
            if (bllQuestion == null)
                return null;
            var mvcQuestion = new QuestionViewModel()
            {
                Id = bllQuestion.Id,
                ThemeId = bllQuestion.ThemeId,
                Text = bllQuestion.Text,
                TestId = bllQuestion.TestId,
                Answers = bllQuestion.Answers.Select(r => r.ToMvcAnswer()).ToList()
            };
            return mvcQuestion;
        }
    }
}