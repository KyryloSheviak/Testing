using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Testing.Domain.Entitys;
using System.Data.Entity;

namespace Testing.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // получения списка тестов
        public ActionResult Tests()
        {
            // если пользователь заблокирован, то перевод на страницу
            var user = context.Users.First(u => u.UserName == User.Identity.Name).isBlock;
            if (user)
            {
                return View("BlockUser");
            }

            // получаем все тесты пройденные юзером
            var late = context
                .TestUsers
                .Where(x => x.ApplicationUserId == User.Identity.Name)
                .Select(s => s.TestId)
                .Distinct()
                .ToList();

            // возвращаем только те тесты которые пользователь не прощел
            var tests = context
                .Tests
                .Where(a => !a.isDelete && !late.Any(e => e == a.Id));

            return View(tests);
        }

        public ActionResult Start(int? id)
        {
            if (id == null) return HttpNotFound();

            var test = context.Tests.Find(id);
            if (test == null) return HttpNotFound();
            
            // заносим инфу о том что пользователь начал тест
            context.TestUsers.Add(new TestUser
            {
                TestId = (int)id,
                ApplicationUserId = User.Identity.Name
            });
            context.SaveChanges();


            // устанавлимаем время для теста и запускаем на js
            ViewBag.Time = test.TimeToGo;
            // название теста
            ViewBag.Subject = test.Subject;
            // количество вопросов
            ViewBag.CountQuestion = test.CountQuestions;
            // номер вопроса
            ViewBag.Num = 1;

            int idquest = test.Questions.First().Id;
            // возвращаем первый вопрос к выбранному тесу
            var Question = new Question
            {
                Id = idquest, // id первого вопроса
                TestId = (int)id, // id теста
                UserQuestion = test.Questions.First().UserQuestion, // первый вопрос
                Answers = context.Answers.Where(p => p.QuestionId == idquest).ToList() // коллекция ответов по вопросу
            };

            return View(Question);
        }

        [HttpPost]
        public ActionResult AddUserAnswer(int idtest, int idquest, int num, List<int> idans)
        {
            // сохраняем результаты в таблицу
            foreach (var item in idans)
            {
                context.AnswersUsers.Add(new AnswersUsers
                {
                    QuestionId = idquest,
                    AnswerId = item,
                    TestId = idtest,
                    ApplicationUserId = User.Identity.Name
                });
            }
            context.SaveChanges();
                                 
            // ===========================================================

            var test = context.Tests.Find(idtest);
            if (test == null) return HttpNotFound();

            if (num == test.Questions.Count)
            {
                // получаем список id вопросов по выбранному тесту
                var ids = context
                    .Questions
                    .Where(q => q.TestId == idtest)
                    .Select(i => i.Id)
                    .ToList();
                // получаем количество правильных вопросов по тесту
                var correctAns = context
                    .Answers
                    .Where(a => ids.Any(r => r == a.QuestionId) && a.IsCorrect)
                    .Count();
                
                // получили id ответов по тесту
                var y = context
                    .AnswersUsers
                    .Where(w => w.ApplicationUserId == User.Identity.Name && w.TestId == idtest)
                    .Select(i => i.AnswerId);
                // получаем кол-во правильных ответов пользователем
                var h = context
                    .Answers
                    .Where(a => a.IsCorrect == true && y.Any(e => e == a.Id))
                    .Count();

                ViewBag.YourAns = h;
                ViewBag.CorrectAns = correctAns;
                return PartialView("_Result");
            }

            int numc = num + 1;
            ViewBag.Num = numc;
            ViewBag.CountQuestion = test.CountQuestions;

            // получаем id след вопроса
            int idq = test.Questions.Skip(numc - 1).First().Id;

            var Question = new Question
            {
                Id = test.Questions.Skip(numc - 1).First().Id, // id вопроса
                TestId = (int)idtest, // id теста
                UserQuestion = test.Questions.Skip(numc -1).First().UserQuestion, // вопрос
                Answers = context.Answers.Where(p => p.QuestionId == idq).ToList() // коллекция ответов по вопросу
            };

            return PartialView("_QuestionPartial", Question);
        }
    }
}