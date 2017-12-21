using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            Question = new HashSet<Question>();
        }

        public int IdQuestionnaire { get; set; }
        public string Text { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}
