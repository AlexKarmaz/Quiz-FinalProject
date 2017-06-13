using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }
        public int Id { get; set; }
        [ForeignKey("Theme")]
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public int? TestId { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}