using System;
using System.Collections.Generic;

namespace question_app.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            UserAnswer = new HashSet<UserAnswer>();
        }

        public int IdQuestions { get; set; }
        public string QuestionText { get; set; }
        public int QuestionnaireIdQuestionnaire { get; set; }

        public Questionnaire QuestionnaireIdQuestionnaireNavigation { get; set; }
        public ICollection<Answer> Answer { get; set; }
        public ICollection<UserAnswer> UserAnswer { get; set; }
    }
}
