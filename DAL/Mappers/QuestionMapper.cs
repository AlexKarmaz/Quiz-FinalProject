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
                throw new ArgumentNullException(nameof(ormQuestion));
            var dalQuestion = new DalQuestion()
            {
                Id = ormQuestion.Id,
                ThemeId = ormQuestion.ThemeId,
                Text = ormQuestion.Text,
                TestId = ormQuestion.TestId,
                Answers = ormQuestion.Answers.Select(r => r.ToDalAnswer()).ToList()
            };
            return dalQuestion;
        }

        public static Question ToOrmQuestion(this DalQuestion dalQuestion)
        {
            if (dalQuestion == null)
                throw new ArgumentNullException(nameof(dalQuestion));
            var ormQuestion = new Question()
            {
                Id = dalQuestion.Id,
                ThemeId = dalQuestion.ThemeId,
                Text = dalQuestion.Text,
                TestId = dalQuestion.TestId,
                Answers = dalQuestion.Answers.Select(r => r.ToOrmAnswer()).ToList()
            };
            return ormQuestion;
        }
    }
}
