using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Testing.Domain.Entitys;
using Testing.Domain.Interface;

namespace Testing.Web.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private IRepositoryAdmin repository;
        public AdminController(IRepositoryAdmin r)
        {
            repository = r;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Get: получение всех юзеров
        public ActionResult Users()
        {
            var c = repository.GetUsers();
            return View(c);
        }

        // блокировка и разблокировка юзера
        public ActionResult BlockUnBlock(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repository.BlockUser(id);
            return View("Users", repository.GetUsers());
        }

        // удаление юзера
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repository.DeleteUser(id);
            return View("Users", repository.GetUsers());
        }

        // получения списка тестов
        public ActionResult Tests()
        {
            return View(repository.GetTests());
        }

        // soft удаления теста
        public ActionResult DeleteTest(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            repository.DeleteTest(id);
            return View("Tests", repository.GetTests());
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.СomplexityId = new SelectList(repository.GetСomplexity(), "Id", "Complication");
            return View();
        }

        // POST: Tests/Create
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,TimeToGo,CountQuestions,СomplexityId")] Test test)
        {
            if (ModelState.IsValid)
            {
                repository.AddTest(test);
                return RedirectToAction("AddInformation/" + test.Id);
            }
            ViewBag.СomplexityId = new SelectList(repository.GetСomplexity(), "Id", "Complication", test.СomplexityId);
            return View(test);
        }

        public ActionResult AddInformation(int? id)
        {
            return View(repository.GetTest(id));
        }

        // добавления вопросв по тесту
        [HttpPost]
        public string AddQuestion(string name, string idtest, List<string> answers, List<string> ans)
        {
            var result = repository.AddQuestion(name, idtest, answers, ans);
            return result.ToString();
        }

        // отображения view с изменение информации о тесте
        [HttpGet]
        public ActionResult EditTestInfo(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var t = repository.GetTest(id);
            if(t == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.СomplexityId = new SelectList(
                repository.GetСomplexity(),
                "Id", 
                "Complication",
                selectedValue: t.СomplexityId);

            return View(t);
        }

        // сохранение информации о тесте
        [HttpPost]
        public ActionResult EditTestInfo(Test test)
        {
            repository.EditTestInformation(test);
            return RedirectToAction("Tests");
        }
    }
}