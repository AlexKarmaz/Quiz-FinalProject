using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    /// <summary>
    /// This ORM entity represents an answer which stores in the database.
    /// </summary>
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
    }
}