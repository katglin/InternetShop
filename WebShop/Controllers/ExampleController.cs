using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BLL;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            using (var domainModel = new ExampleDomainModel())
            {
                var model = domainModel.GetAll();
                return View(model);
            }
        }

        public ActionResult AddOne()
        {
            using (var domainModel = new ExampleDomainModel())
            {
                domainModel.AddExample();
                return RedirectToAction("Index");
            }
        }
    }
}