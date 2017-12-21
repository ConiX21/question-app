using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class Reponse
    {
        public int IdReponse { get; set; }
        public string ValeurReponse { get; set; }
        public bool TrueReponse { get; set; }
        public int QuestionReponseReponseIdQuestions { get; set; }

        public Question QuestionReponseReponseIdQuestionsNavigation { get; set; }
    }
}
