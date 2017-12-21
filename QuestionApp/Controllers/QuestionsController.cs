using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionApp.Models;

namespace QuestionApp.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly questiondbContext _context;

        public QuestionsController(questiondbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var user = User.Claims.ElementAt(0).Value ?? "";
            var passed = _context.UtilisateurReponse.Any(a => a.IdQuestionnaire == id && a.AspNetUsersId == user);

            if (!passed)
            {
                var QuestionVM = new QuestionViewModel();

                QuestionVM.UtilisateurReponses = new List<UtilisateurReponse>();

                for (int i = 0; i < _context.Question.Where(q => q.QuestionnaireIdQuestionnaire == id).Count(); i++)
                {
                    QuestionVM.UtilisateurReponses.Add(new UtilisateurReponse());
                }

                foreach (var item in _context.Question.Include("Reponse").Where(q => q.QuestionnaireIdQuestionnaire == id))
                {
                    QuestionVM.Questions.Add(item);
                }

                ViewBag.IdQuestionnaire = id;
                ViewBag.Questionnaire = _context.Questionnaire.Find(id).Text;


                return View(QuestionVM);
            }
            else
            {
                return RedirectToAction("ResultTest", new { id = id });
            }



        }

        [HttpPost]
        public IActionResult Index(QuestionViewModel qa)
        {

            var user = User.Claims.ElementAt(0).Value;
            var id = 0;
            foreach (var item in qa.UtilisateurReponses)
            {
                id = item.IdQuestionnaire;
                item.AspNetUsersId = user;
                _context.UtilisateurReponse.Add(item);
            }

            _context.SaveChanges();


            return RedirectToAction("ResultTest", new { id = id });
        }

        public IActionResult ResultTest(int id)
        {
            /*SELECT u.*, r.TrueReponse FROM UtilisateurReponse u
                JOIN question q on u.Question_IdQuestions = q.IdQuestions
                JOIN reponse r ON u.ValeurReponseUtilisateur = r.IdReponse
                WHERE IDQuestionnaire = 2;*/
            var user = User.Claims.ElementAt(0).Value;

            var stats = from u in _context.UtilisateurReponse
                        join q in _context.Question on u.QuestionIdQuestions equals q.IdQuestions
                        join r in _context.Reponse on u.ValeurReponseUtilisateur equals r.IdReponse
                        where u.IdQuestionnaire == id && u.AspNetUsersId == user
                        select new { reponse = r, utilisateurReponse = u, question = q };

            var countQuestionTrue = stats.Where(s => s.reponse.TrueReponse == true).Count();

            var resultVM = new ResultViewModel()
            {
                PercentResult = (countQuestionTrue / (decimal)stats.Count()) * 100,
                Questionnaire = _context.Questionnaire.Find(id).Text
            };

            if (resultVM.PercentResult >= 75)
                ViewBag.classStat = "text-success";
            else if (resultVM.PercentResult < 75 && resultVM.PercentResult >= 50)
                ViewBag.classStat = "text-primary";
            else if (resultVM.PercentResult < 50 && resultVM.PercentResult >= 25)
                ViewBag.classStat = "text-warning";
            else if (resultVM.PercentResult < 25)
                ViewBag.classStat = "text-danger";


            return View(resultVM);
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details()
        {
            var user = User.Claims.ElementAt(0).Value;

            var stats = from u in _context.UtilisateurReponse
                        join q in _context.Question on u.QuestionIdQuestions equals q.IdQuestions
                        join r in _context.Reponse on u.ValeurReponseUtilisateur equals r.IdReponse
                        select new { reponse = r, utilisateurReponse = u, question = q };

            var countQuestionTrue = stats.Where(s => s.reponse.TrueReponse == true).Count();

            //var resultVM = new ResultViewModel()
            //{
            //    PercentResult = (countQuestionTrue / (decimal)stats.Count()) * 100,
            //    Questionnaire = _context.Questionnaire.Find(id).Text
            //};

            return View();
        }


    }
}
