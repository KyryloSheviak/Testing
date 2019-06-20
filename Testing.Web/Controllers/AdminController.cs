using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Entitys;

namespace Testing.Web.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Get: получение всех юзеров
        public ActionResult Users()
        {
            var c = context.Users.Where(u => !u.isDelete);
            return View(c);
        }

        // блокировка и разблокировка юзера
        public ActionResult BlockUnBlock(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var t = context.Users.Find(id).isBlock;
            context.Users.Find(id).isBlock = !t;
            context.SaveChanges();
            return View("Users", context.Users.Where(u => !u.isDelete));
        }

        // удаление юзера
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            context.Users.Find(id).isDelete = true;
            context.SaveChanges();
            return View("Users", context.Users.Where(u => !u.isDelete));
        }

        // получения списка тестов
        public ActionResult Tests()
        {
            return View(context.Tests.Where(t => !t.isDelete));
        }

        // soft удаления теста
        public ActionResult DeleteTest(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            context.Tests.Find(id).isDelete = true;
            context.SaveChanges();
            return View("Tests", context.Tests.Where(t => !t.isDelete));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.СomplexityId = new SelectList(context.Сomplexitys, "Id", "Complication");
            return View();
        }

        // POST: Tests/Create
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,TimeToGo,CountQuestions,СomplexityId")] Test test)
        {
            if (ModelState.IsValid)
            {
                context.Tests.Add(test);
                context.SaveChanges();
                return RedirectToAction("AddInformation/" + test.Id);
            }

            ViewBag.СomplexityId = new SelectList(context.Сomplexitys, "Id", "Complication", test.СomplexityId);
            return View(test);
        }

        public ActionResult AddInformation(int? id)
        {
            return View(context.Tests.Find(id));
        }

        // добавления вопросв по тесту
        [HttpPost]
        public string AddQuestion(string name, string idtest, List<string> answers, List<string> ans)
        {
            int testID = Convert.ToInt32(idtest);
            var obj = new Question
            {
                TestId = Convert.ToInt32(idtest),
                UserQuestion = name
            };
            context.Questions.Add(obj);
            context.SaveChanges();

            int id = obj.Id;

            var Answers = new List<Answer>();
            for (int i = 0; i < answers.Count; i++)
            {
                Answers.Add(new Answer
                {
                    QuestionId = id,
                    UserAnswer = answers[i].ToString(),
                    IsCorrect = ans[i] == "1" ? true : false
                });
            }

            Answers.ForEach(t => context.Answers.Add(t));
            context.SaveChanges();

            var countQuestions = context.Tests.Find(testID).Questions.Count();
            context.Tests.Find(testID).CountQuestions = countQuestions;
            context.SaveChanges();
            return countQuestions.ToString();
        }

        // отображения view с изменение информации о тесте
        [HttpGet]
        public ActionResult EditTestInfo(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var t = context.Tests.Find(id);
            if(t == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.СomplexityId = new SelectList(
                context.Сomplexitys, 
                "Id", 
                "Complication",
                selectedValue: t.СomplexityId);

            return View(t);
        }

        // сохранение информации о тесте
        [HttpPost]
        public ActionResult EditTestInfo(Test test)
        {
            context.Tests.Find(test.Id).Subject = test.Subject;
            context.Tests.Find(test.Id).TimeToGo = test.TimeToGo;
            context.Tests.Find(test.Id).СomplexityId = test.СomplexityId;
            context.SaveChanges();

            return RedirectToAction("Tests");
        }


    }
}