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

        public ActionResult Error()
        {
            return View();
        }
    }
}
