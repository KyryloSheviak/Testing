using System.Collections.Generic;
using System.Web.Mvc;
using Testing.Domain.Interface;

namespace Testing.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private IRepositoryUser repository;
        public UserController(IRepositoryUser r)
        {
            repository = r;
        }

        public ActionResult Index()
        {
            ViewBag.CountTests = repository.CountTests(User.Identity.Name);
            return View();
        }

        // получения списка тестов
        public ActionResult Tests()
        {
            if(repository.IsBlockUser(User.Identity.Name))
                return View("BlockUser");

            return View(repository.FirstOpenPage(User.Identity.Name));
        }

        public ActionResult Start(int? id)
        {
            var b = repository.Test(id, User.Identity.Name);
            if (b)
            {
                return RedirectToAction("Tests");
            }

            var result = repository.StartTest(id, User.Identity.Name);
            ViewBag.Time = result.time;
            // название теста
            ViewBag.Subject = result.subject;
            // количество вопросов
            ViewBag.CountQuestion = result.countQuestions;
            // номер вопроса
            ViewBag.Num = 1;

            return View(result.question);
        }

        [HttpPost]
        public ActionResult AddUserAnswer(int idtest, int idquest, int num, List<int> idans)
        {
            repository.SaveUserAnswer(idtest, idquest, idans, User.Identity.Name);
            
            var test = repository.CountQuestions(idtest); //context.Tests.Find(idtest);

            if (num == test)
            {
                ViewBag.YourAns = repository.Result(idtest, User.Identity.Name);
                return PartialView("_Result");
            }

            int numc = num + 1;
            ViewBag.Num = numc;
            ViewBag.CountQuestion = test;
           
            var result = repository.NextQuestion(idtest, numc);

            return PartialView("_QuestionPartial", result);
        }

        // результаты по тестам
        public ActionResult Results()
        {
            var result = repository.AllResults(User.Identity.Name);

            return View(result);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(string subject, string level)
        {
            return View("Tests", repository.GetTestForUser(User.Identity.Name, subject, level));
        }
    }
}