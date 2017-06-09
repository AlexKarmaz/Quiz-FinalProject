using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class AnswerMapper
    {
        public static DalAnswer ToDalAnswer(this Answer ormAnswer)
        {
            if (ormAnswer == null)
                return null;
            var dalAnswer = new DalAnswer()
            {
                Id = ormAnswer.Id,
                Text = ormAnswer.Text,
                IsRight = ormAnswer.IsRight,
                QuestionId = ormAnswer.QuestionId
            };
            return dalAnswer;
        }

        public static Answer ToOrmAnswer(this DalAnswer dalAnswer)
        {
            if (dalAnswer == null)
                return null;
            var ormAnswer = new Answer()
            {
                Id = dalAnswer.Id,
                Text = dalAnswer.Text,
                IsRight = dalAnswer.IsRight,
                QuestionId = dalAnswer.QuestionId
            };
            return ormAnswer;
        }
    }
}
