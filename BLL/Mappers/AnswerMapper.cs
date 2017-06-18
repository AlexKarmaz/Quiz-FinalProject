using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class AnswerMapper
    {
        public static DalAnswer ToDalAnswer(this BllAnswer bllAnswer)
        {
            if (bllAnswer == null)
                throw new ArgumentNullException(nameof(bllAnswer));
            var dalAnswer = new DalAnswer()
            {
                Id = bllAnswer.Id,
                Text = bllAnswer.Text,
                IsRight = bllAnswer.IsRight,
                QuestionId = bllAnswer.QuestionId
            };
            return dalAnswer;
        }

        public static BllAnswer ToBllAnswer(this DalAnswer dalAnswer)
        {
            if (dalAnswer == null)
                throw new ArgumentNullException(nameof(dalAnswer));
            var bllAnswer = new BllAnswer()
            {
                Id = dalAnswer.Id,
                Text = dalAnswer.Text,
                IsRight = dalAnswer.IsRight,
                QuestionId = dalAnswer.QuestionId
            };
            return bllAnswer;
        }
    }
}
