using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionApp.Models;

namespace QuestionApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var QuestionVM = new QuestionViewModel();
            using (var ctx = new questiondbContext())
            {
                QuestionVM.UtilisateurReponses = new List<UtilisateurReponse>();

                for (int i = 0; i < ctx.Question.Count(); i++)
                {
                    QuestionVM.UtilisateurReponses.Add(new UtilisateurReponse());
                }

                foreach (var item in ctx.Question.Include("Reponse"))
                {
                    QuestionVM.Questions.Add(item);
                }
            }

            return View(QuestionVM);
        }

        [HttpPost]
        public IActionResult Index(QuestionViewModel qa)
        {
          
            var user = User.Claims.ElementAt(0).Value;
            using (var ctx = new questiondbContext())
            {
                foreach (var item in qa.UtilisateurReponses)
                {
                    item.AspNetUsersId = user;
                    ctx.UtilisateurReponse.Add(item);
                }

                ctx.SaveChanges();

            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
