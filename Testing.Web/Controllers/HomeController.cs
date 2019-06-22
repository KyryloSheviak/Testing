using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testing.Domain.Entitys;
using Testing.Domain.Interface;
using Testing.Web.Models;

namespace Testing.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryUser repository;
        public HomeController(IRepositoryUser r)
        {
            repository = r;
        }

        public ActionResult Index()
        {
            return View();
        }

        /*
        public ActionResult Test()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var r = context.Tests; //.Where(p => p.Subject == "Математика");

            List<string> compModels =
                context
                .Tests
                .Select(x => x.Subject)
                .Distinct()
                .ToList();
            compModels.Insert(0, "Все...");

            TestsViewModel ivm = new TestsViewModel
            {
                Subject = compModels, Test = r
            };

            return View(ivm);
        }
        */

        [HttpPost]
        public ActionResult Submit(string subject)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            /*
            if (subject == "Все...")
                var c = context.Tests.Where(cc => !cc.isDelete);
            else
                var t = context.Tests.Where(cc => !cc.isDelete && cc.Subject == subject);
            
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
        */

            List<string> compModels =
                context
                .Tests
                .Select(x => x.Subject)
                .Distinct()
                .ToList();
            compModels.Insert(0, "Все...");

            TestsViewModel ivm = new TestsViewModel
            {
                Subject = compModels,
                Test = null
            };

            return View("Test", ivm);
        }
    }
}