using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class UtilisateurReponse
    {
        public int IdUtilistaeurReponse { get; set; }
        public int ValeurReponseUtilisateur { get; set; }
        public int QuestionIdQuestions { get; set; }
        public string AspNetUsersId { get; set; }
        public int IdQuestionnaire { get; set; }

        public Question QuestionIdQuestionsNavigation { get; set; }
    }
}
