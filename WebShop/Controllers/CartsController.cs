using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using WebShop.BLL;

namespace WebShop.Controllers
{
    public class CartsController : Controller
    {
        [HttpPost]
        public string Update(long cartmodel_id,int status_new,DateTime date)
        {
            using (var domainModel = new CartsDomainModel())
            {
                domainModel.Update(domainModel.Get(cartmodel_id),status_new,date);
                var domainmodel1 = new OrderDomainModel();
                var c = new WebShop.Content.First();
                if (status_new == 3)
                {
                    OrderModel o = domainmodel1.GetOne(cartmodel_id);
                    string m = "Ваш заказ отправлен! ";
                    
                    c.SendEmailaboutStatus(o,m);
                }
                if (status_new == 4)
                {
                    c.SendEmailaboutStatus(domainmodel1.GetOne(cartmodel_id), "Ваш заказ удовлетворить невозможно!");
                    //RedirectToAction("SendEmailaboutStatus", "Order", new { order = domainmodel1.GetOne(cartmodel_id), message_about_status = "Ваш заказ удовлетворить невозможно!" });
                }

                return "Изменения внесены";
            }
        }
        public ActionResult Delete_carts()
        {
            var domainmodel2 = new OrderCartDomainModel();
            var domainmodel3 = new OrderDomainModel();
            var list1 = domainmodel2.GetAll();
            var domainmodel1 = new CartsDomainModel();
            
                //var model = repository1.Query().Where(x => x.Cart_id == id).Select(x => new Models.CartModel
                //{
                //    Cart_id = x.Cart_id,
                //    State = x.State,
                //    Artikul = x.Artikul,
                //    IP = x.IP,
                //    Number = x.Number,
                //    User_id = x.User_id,
                //    date = x.Order_date
                //}).FirstOrDefault();
                //repository1.Delete(model.Cart_id);
                var list = domainmodel1.GetAll();
                foreach (var l in list)
                {
                    if (l.State == 3)
                    {
                        domainmodel1.Delete(l.Cart_id);
                        var list1_dev = list1.FirstOrDefault(x=>x.Cart_id==l.Cart_id);
                        domainmodel3.Delete(list1_dev.Order_id);
                    }
                    
                }
                return RedirectToAction("AllOrders","Order");
            
        }
        public void CartAmount()
        {
            int amount = 0;
            using (var domainModel = new CartsDomainModel())
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    amount = domainModel.CartAmount(true, System.Web.HttpContext.Current.User.Identity.GetUserId());
                else
                {
                    amount = domainModel.CartAmount(false, System.Web.HttpContext.Current.Request.UserHostAddress);
                }
            }
            System.Web.HttpContext.Current.Session["Carts"] = amount;
          //  Session["Carts"] = amount;
        }

        public ActionResult userChoice(long? prId, bool confirm)
        {
            return AddItem(prId, confirm);
        }

        public ActionResult AddItem(long? prId, bool? confirm)
        {
            using (var domainModel = new CartsDomainModel())
            {
                if (User.Identity.IsAuthenticated)
                {
                    domainModel.AddItem((long)prId, true, User.Identity.GetUserId());                    
                }
                else
                {
                    domainModel.AddItem((long)prId, false,
                        System.Web.HttpContext.Current.Request.UserHostAddress);
                }          

            }
            CartAmount(); // change counter on icon of the Cart
            if (confirm != null && confirm == true)
                return RedirectToAction("Cart");
            else
                return Redirect(Request.UrlReferrer.AbsoluteUri); 
        }

        public ActionResult Save(long cart_id, int number)
        {
            using (var domainModel = new CartsDomainModel())
            {
                domainModel.Save(cart_id, number);
            }
            CartAmount(); // change counter on icon of the Cart
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult Delete(long cart_id)
        {
            using (var domainModel = new CartsDomainModel())
            {
                domainModel.Delete(cart_id);
            }
            CartAmount(); // change counter on icon of the Cart
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpGet]
        public ActionResult Cart()
        {
            CartAmount(); // change counter on icon of the Cart
            using (var domainModel = new CartsDomainModel())
            {
                string user_aut;
                if (User.Identity.IsAuthenticated)
                    user_aut = User.Identity.GetUserId();
                else user_aut = Request.UserHostAddress;
                var list = domainModel.OpenCart(user_aut).ToList();
                return View(list);
            }    
        }

        [HttpPost]
        public ActionResult Cart(List <CartModel> carts)
        {
            List<Int64> ids = new List<long>();
            if (carts != null)
            {
                foreach (var cart in carts)
                {
                    if (cart.Buy == true)
                        ids.Add(cart.Cart_id);
                }
                if(ids!=null && ids.Count>0)
                {
                    TempData["ids"] = ids;
                    return RedirectToAction("OrderForm", "Order");
                }
                else return Redirect(Request.UrlReferrer.AbsoluteUri);
            }
            else return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}