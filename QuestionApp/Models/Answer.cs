using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class Answer
    {
        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
        public int QuestionIdQuestions { get; set; }
        public bool TrueAnswer { get; set; }

        public Question QuestionIdQuestionsNavigation { get; set; }
    }
}
