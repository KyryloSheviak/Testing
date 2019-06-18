using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Entitys;
using Testing.Web.Models;

namespace Testing.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var r = context.Tests;

            List<Сomplexity> compModels =
                context
                .Сomplexitys
                // .OrderBy(o => o.Complication)
                .Distinct()
                .ToList();
            compModels.Insert(0, new Сomplexity { Id = 0, Complication = "Все..." });

            /*
            // формируем список компаний для передачи в представление
            List<Сomplexity> compModels = 
                context
                .Сomplexitys
                .Select(c => new Сomplexity { Id = c.Id, Complication = c.Complication })
                .ToList();
            // добавляем на первое место
            */
            TestsViewModel ivm = new TestsViewModel
            {
                Сomplexity = compModels, Test = r
            };

            return View(ivm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();

            ApplicationDbContext context = new ApplicationDbContext();
            var c = context.Questions.Where(o => o.TestId == id);
            return View(c);
        }

        [HttpPost]
        public ActionResult Submit(FormCollection formcollection)
        {
            string sub = formcollection["Subject"];
            TempData["Message"] = sub;
            ApplicationDbContext context = new ApplicationDbContext();
            var c = context.Tests.Where(cc => cc.Subject == sub);

            // предметы
            SelectList subject = new SelectList(
                context
                .Tests
                 //.OrderBy(o => o.Subject)
                .Select(p => p.Subject)
                .Distinct()
                .ToList()
            );
            ViewBag.Subject = subject;

            // сложность
            SelectList complexity = new SelectList(
                context
                .Сomplexitys
               // .OrderBy(o => o.Complication)
                .Select(p => p.Complication)
                .Distinct()
                .ToList()
            );
            ViewBag.Сomplexity = complexity;

            return View("Test", c);
        }
    }
}