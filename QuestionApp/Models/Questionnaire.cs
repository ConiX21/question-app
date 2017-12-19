using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            Question = new HashSet<Question>();
            UserAnswer = new HashSet<UserAnswer>();
        }

        public int IdQuestionnaire { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Question { get; set; }
        public ICollection<UserAnswer> UserAnswer { get; set; }
    }
}
