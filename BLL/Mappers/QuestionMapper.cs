using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class QuestionMapper
    {
        public static DalQuestion ToDalQuestion(this BllQuestion bllQuestion)
        {
            if (bllQuestion == null)
                return null;
            var dalQuestion = new DalQuestion()
            {
                Id = bllQuestion.Id,
                ThemeId = bllQuestion.ThemeId,
                Text = bllQuestion.Text,
                Answers = bllQuestion.Answers.Select(r => r.ToDalAnswer()).ToList()
            };
            return dalQuestion;
        }

        public static BllQuestion ToBllQuestion(this DalQuestion dalQuestion)
        {
            if (dalQuestion == null)
                return null;
            var bllQuestion = new BllQuestion()
            {
                Id = dalQuestion.Id,
                ThemeId = dalQuestion.ThemeId,
                Text = dalQuestion.Text,
                Answers = dalQuestion.Answers.Select(r => r.ToBllAnswer()).ToList()
            };
            return bllQuestion;
        }
    }
}
