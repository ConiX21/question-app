using System;
using System.Collections.Generic;

namespace QuestionApp.Data
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
