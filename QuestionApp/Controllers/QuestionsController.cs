using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionApp.Models;

namespace QuestionApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly questiondbContext _context;

        public QuestionsController(questiondbContext context)
        {
            _context = context;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var questiondbContext = _context.Question
                                            .Include(q => q.QuestionnaireIdQuestionnaireNavigation)
                                            .Include(q => q.Answer)
                                            .Where(q => q.QuestionnaireIdQuestionnaire == 1);
            return View(await questiondbContext.ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.QuestionnaireIdQuestionnaireNavigation)
                .SingleOrDefaultAsync(m => m.IdQuestions == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewData["QuestionnaireIdQuestionnaire"] = new SelectList(_context.Questionnaire, "IdQuestionnaire", "IdQuestionnaire");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuestions,QuestionText,QuestionnaireIdQuestionnaire")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionnaireIdQuestionnaire"] = new SelectList(_context.Questionnaire, "IdQuestionnaire", "IdQuestionnaire", question.QuestionnaireIdQuestionnaire);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.SingleOrDefaultAsync(m => m.IdQuestions == id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["QuestionnaireIdQuestionnaire"] = new SelectList(_context.Questionnaire, "IdQuestionnaire", "IdQuestionnaire", question.QuestionnaireIdQuestionnaire);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuestions,QuestionText,QuestionnaireIdQuestionnaire")] Question question)
        {
            if (id != question.IdQuestions)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.IdQuestions))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionnaireIdQuestionnaire"] = new SelectList(_context.Questionnaire, "IdQuestionnaire", "IdQuestionnaire", question.QuestionnaireIdQuestionnaire);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.QuestionnaireIdQuestionnaireNavigation)
                .SingleOrDefaultAsync(m => m.IdQuestions == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.SingleOrDefaultAsync(m => m.IdQuestions == id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.IdQuestions == id);
        }
    }
}
