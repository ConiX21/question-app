using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionApp.Models
{
    public class QuestionViewModel
    {
        public List<Question> Questions { get; set; }
        public List<UtilisateurReponse> UtilisateurReponses { get; set; }
        
        public QuestionViewModel()
        {
            Questions = new List<Question>();
               UtilisateurReponses = new List<UtilisateurReponse>();

        }
    }
}
