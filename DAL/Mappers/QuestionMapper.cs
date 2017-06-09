using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class QuestionMapper
    {
        public static DalQuestion ToDalQuestion(this Question ormQuestion)
        {
            if (ormQuestion == null)
                return null;
            var dalQuestion = new DalQuestion()
            {
                Id = ormQuestion.Id,
                ThemeId = ormQuestion.ThemeId,
                Text = ormQuestion.Text,
                Answers = ormQuestion.Answers.Select(r => r.ToDalAnswer()).ToList()
            };
            return dalQuestion;
        }

        public static Question ToOrmQuestion(this DalQuestion dalQuestion)
        {
            if (dalQuestion == null)
                return null;
            var ormQuestion = new Question()
            {
                Id = dalQuestion.Id,
                ThemeId = dalQuestion.ThemeId,
                Text = dalQuestion.Text,
                Answers = dalQuestion.Answers.Select(r => r.ToOrmAnswer()).ToList()
            };
            return ormQuestion;
        }
    }
}
