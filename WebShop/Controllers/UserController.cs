using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebShop.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize(Roles ="superadmin")]
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            //ViewBag.item = db.Users.ToList();
            var model = db.Users.ToList();
            List<UserWrapper> UserWrappers = new List<UserWrapper>();
            foreach (var user in model)
            {
                UserWrappers.Add(new UserWrapper { appUser = user, CurrentRole = user.Roles.FirstOrDefault().RoleId });
            }
            return View(UserWrappers);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Index_for_admin()
        {
            var db = new ApplicationDbContext();
            var model = db.Users.ToList();
            List<UserWrapper> UserWrappers = new List<UserWrapper>();
            foreach (var user in model)
            {
                UserWrappers.Add(new UserWrapper { appUser = user, CurrentRole = user.Roles.FirstOrDefault().RoleId });
            }
            return View(UserWrappers);
        }
        [HttpPost]
        [Authorize(Roles = "superadmin")]
        public string Update(string email, string status_new)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            UserWrapper uw = new UserWrapper();
            
            string RoleName = db.Roles.Where(x=>x.Id==status_new).First().Name;
            foreach (var i2 in userManager.Users.ToList())
            {
                uw.appUser = i2;
                uw.CurrentRole = i2.Roles.First().RoleId;
                if (i2.Email == email)
                {
                        string RoleName1 = db.Roles.Where(x => x.Id == uw.CurrentRole).First().Name;
                        userManager.RemoveFromRole(i2.Id, RoleName1); // я не уверена что тут должен быть RoleId - ПРОВЕРЬ ПЛИЗ
                    userManager.AddToRole(i2.Id, RoleName); // возможно тут не RoleName, а RoleId
                }
            }
                
            db.SaveChanges();
            return ("Изменения внесены!");
        }
    }
}