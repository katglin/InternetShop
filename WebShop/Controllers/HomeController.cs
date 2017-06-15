using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BLL;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using System.Web.Security;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (User.IsInRole("superadmin"))
            {
                return RedirectToAction("Index", "User");
            }
            else if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index_for_admin", "User");
            }
            else
            {
                //return RedirectToAction("Index", "Home");
                using (var domainModel = new BannerDomainModel())
                {
                    ViewBag.MenuItem = "Main";
                    var list = domainModel.GetAll();
                    (new CartsController()).CartAmount();
                    return View(list);
                }
            }  
        }

        #region maybeusedlater
        // use this to test sending of image id
        //public string TestButtonToShop(long? myId)
        //{
        //    if (myId != null)
        //        return myId.ToString();
        //    else return "no id";
        //}

        // added to test choice of category
        //public ActionResult ShowCategory(long? myId)
        //{
        //    if (myId != null)
        //        ViewBag.ID = myId;
        //    return View("Index");
        //}
        #endregion

        //[Authorize(Roles = "admin")]
        public ActionResult About()
        {
            (new CartsController()).CartAmount();
            using (var domainModel = new StaticDomainModel())
            {
                var list = domainModel.GetAll();
                ViewBag.MenuItem = "About";
                return View(list);
            }
        }

        public ActionResult Sales()
        {
            (new CartsController()).CartAmount();
            using (var domainModel = new StaticDomainModel())
            {
                var list = domainModel.GetAll();
                ViewBag.MenuItem = "Sales";
                return View(list);
            }
        }
        
        public ActionResult Delivery()
        {
            (new CartsController()).CartAmount();
            using (var domainModel = new StaticDomainModel())
            {
                var list = domainModel.GetAll();
                ViewBag.MenuItem = "Delivery";
                return View(list);
            }
        }
[HttpGet]
        public ViewResult EditAll()
        {
            // Static_id static_id = db.Statics.Find(static_id);
            using (var domainModel = new StaticDomainModel())
            {
                var list = domainModel.GetAll();
                return View(list);
            }
                    }

        [HttpPost]
        [ValidateInput(false)]
        public string EditAll(StaticModel staticmodel)
        {

            try
            {
                var domainModel = new StaticDomainModel();
                domainModel.AddStatic(staticmodel);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Изменения внесены";
            /*

            if (staticmodel != null)

            {
                return RedirectToAction("EditAll");

            }*/

            //return HttpNotFound();


        }

    }
}