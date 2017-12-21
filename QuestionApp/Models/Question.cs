using System;
using System.Collections.Generic;

namespace QuestionApp.Models
{
    public partial class Question
    {
        public Question()
        {
            Reponse = new HashSet<Reponse>();
            UtilisateurReponse = new HashSet<UtilisateurReponse>();
        }

        public int IdQuestions { get; set; }
        public string Text { get; set; }
        public int QuestionnaireIdQuestionnaire { get; set; }

        public Questionnaire QuestionnaireIdQuestionnaireNavigation { get; set; }
        public ICollection<Reponse> Reponse { get; set; }
        public ICollection<UtilisateurReponse> UtilisateurReponse { get; set; }
    }
}
