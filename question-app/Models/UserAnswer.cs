using System;
using System.Collections.Generic;

namespace question_app.Models
{
    public partial class UserAnswer
    {
        public int IdUserAnswer { get; set; }
        public string AspNetUsersId { get; set; }
        public int QuestionnaireIdQuestionnaire { get; set; }
        public int Answer { get; set; }
        public int QuestionIdQuestions { get; set; }

        public AspNetUsers AspNetUsers { get; set; }
        public Question QuestionIdQuestionsNavigation { get; set; }
        public Questionnaire QuestionnaireIdQuestionnaireNavigation { get; set; }
    }
}
