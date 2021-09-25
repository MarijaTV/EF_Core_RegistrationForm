using System.Linq;
using EF_Core_RegistrationForm.Data;
using EF_Core_RegistrationForm.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_RegistrationForm.Controllers
{
    public class RegistrationFormController : Controller
    {
        private readonly DataContext _context;

        public RegistrationFormController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var rf = new RegistrationFormDTO()
            {
                QuestionAnswers = _context.QuestionAnswers.ToList()
            };
            return View(rf);
        }

        public IActionResult Edit(int id)
        {
            var questionsAnswers = _context.QuestionAnswers.Include(c => c.Question).ThenInclude(q => q.Answers);

            if (id != 0)
            {
                questionsAnswers.Where(qa => qa.RegistrationId == id);
            }
            var questionsWithAnswersIds = _context.QuestionAnswers.Select(s => s.QuestionId).ToList();

            var questionsWithNoAnswers = _context.Questions.Include(q => q.Answers).Where(q => !questionsWithAnswersIds.Contains(q.Id)).ToList();

            var questionAnswers = _context.QuestionAnswers.Include(qa => qa.Question).ThenInclude(q => q.Answers).Include(qa => qa.Answer).ToList();

            foreach (var question in questionsWithNoAnswers)
            {
                questionAnswers.Add(new Models.QuestionAnswer()
                {
                    Question = question,
                    QuestionId = question.Id
                });
            }
            var RegistrationFormDto = new RegistrationFormDTO()
            {
                QuestionAnswers = questionsAnswers.ToList()
            }; //mappinimas iš dtos


            return View(RegistrationFormDto);
        }
        [HttpPost]
        public IActionResult Edit(RegistrationFormDTO registrationForm)
        {
            if (registrationForm.RegistrationId == 0)
            {
                registrationForm.RegistrationId = 1;
            }

            var registration = _context.Registrations.Include(r => r.QuestionAnswers).FirstOrDefault(r => r.Id == registrationForm.RegistrationId);

            registration.QuestionAnswers = registrationForm.QuestionAnswers;

            _context.Registrations.Update(registration);


            _context.SaveChanges();

            return RedirectToAction("Edit");
        }
    }
}
