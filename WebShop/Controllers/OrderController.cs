using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebShop.BLL;
using WebShop.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace WebShop.Controllers
{
    public class OrderController : Controller
    {
        //public ActionResult Update(OrderModel order, string message_about_status)
        //{
        //    SendEmailaboutStatus(order, message_about_status);
        //    using (var orderForm = new OrderDomainModel())
        //    {
        //        return View(orderForm.GetAll());
        //    }
        //}
        public ActionResult AllOrders()
        {
            using (var orderForm = new OrderDomainModel())
            {
                return View(orderForm.GetAll());
            }
        }
        public ActionResult GoToCarts(long Order_id)
        {
            using (var domainModel = new ProductDomainModel())
            {
                var model = domainModel.GetAll();
            
                using (var cartsdm = new CartsDomainModel())
            {
                    ViewBag.Products = model;
                return View(cartsdm.GetByOrderId(Order_id));
            }
            }
        }

        public ActionResult Login()
        {
            return RedirectToAction("Login", "Account", new { returnUrl = Request.UrlReferrer.ToString() });
        }

        [HttpGet]
        public ActionResult OrderForm()
        {
            List<Int64> ids = new List<long>();
            OrderFormModel orderForm = new OrderFormModel();
            if (TempData["ids"]!=null)
            {
                ids = TempData["ids"] as List<Int64>;
                TempData["ids"] = ids;
            }

            var db = new ApplicationDbContext();
            string userId;

            if (User.Identity.IsAuthenticated)
            {
                userId = User.Identity.GetUserId();
                var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                orderForm.Surname = user.Surname;
                orderForm.Name = user.Name;
                orderForm.Address = user.Country + " " + user.City;
                orderForm.Phone = user.Phone;
                orderForm.Email = user.Email;
            }
            orderForm.Cart_ids = new List<long>();
            orderForm.Cart_ids = ids;
            using (var domainModel = new CartsDomainModel())
            {
                orderForm.OrderSum = domainModel.CalcOrderSum(ids);
            }
            return View(orderForm);
        }

        [HttpPost]
        public ActionResult OrderForm(OrderFormModel order)
        {
            if (!ModelState.IsValid)
            {
                using (var domainModel = new CartsDomainModel())
                {
                    order.OrderSum = domainModel.CalcOrderSum(order.Cart_ids);
                }
                return View(order);
            }
            using (var domainModel = new CartsDomainModel())
            {
                if (!domainModel.AddOrderForm(order))
                {
                    ViewBag.Error = "К сожалению, при отправке данных в БД произошла ошибка."+ 
                        " Попробуйте повторить отправку формы";
                    return View(order);
                }
            }
            /* 
              I don`t low Amount_store, increase Popularity of product, set OrderDate - do this on admin page
            */
            (new CartsController()).CartAmount();
            TempData["OrderOK"] = "Ваш заказ будет обработан администраторами в ближайшее время. " +
                "Спасибо, что выбрали наш магазин!";

            SendEmail(order);
            return RedirectToAction("Index", "Home");
        }

        public bool SendEmail(OrderFormModel order)
        {
            string[] shoppingList;
            using (var domainModel = new CartsDomainModel())
            {
                order.OrderSum = domainModel.CalcOrderSum(order.Cart_ids);
                shoppingList = domainModel.getShoppingList(order.Cart_ids);
            }
            string OurName = "Интернет-магазин 'КомпМаг'";
            string OurEmail = "katerinaglinskaya96@gmail.com";
            string OurMess = string.Format("<p>Вы оформили заказ в нашем магазине на сумму {0} грн. " +
                "Наши сотрудники позвонят вам, как только обработают заказ. Обработка заказа " +
                "занимает в среднем 2 дня. Спасибо, что выбрали наш магазин! </p>" +
                "<p>Список заказанных товаров:</p>", order.OrderSum);
            foreach(string s in shoppingList)
            {
                OurMess += "<p>" + s + "</p>";
            }
            var body = "<p>Email От: {0} ({1})</p><p> Здравствуйте, {2}!</p><p>{3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(order.Email));
            message.From = new MailAddress(OurEmail);
            message.Subject = "Оформление заказа в Интеренет-магазине 'КомпМаг'";
            message.Body = string.Format(body, OurName, OurEmail, 
                order.Name + " " + order.Surname, OurMess);
            message.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                    
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendEmailaboutStatus(OrderModel order, string message_about_status)
        {
            //string[] shoppingList;
            //using (var domainModel = new CartsDomainModel())
            //{
            //    order.OrderSum = domainModel.CalcOrderSum(order.Cart_ids);
            //    shoppingList = domainModel.getShoppingList(order.Cart_ids);
            //}
            string OurName = "Интернет-магазин 'КомпМаг'";
            string OurEmail = "katerinaglinskaya96@gmail.com";
            string OurMess = message_about_status;
            //foreach (string s in shoppingList)
            //{
            //    OurMess += "<p>" + s + "</p>";
            //}
            var body = "<p>Email От: {0} ({1})</p><p> Здравствуйте, {2}!</p><p>{3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(order.Email));
            message.From = new MailAddress(OurEmail);
            message.Subject = "Оформление заказа в Интеренет-магазине 'КомпМаг'";
            message.Body = string.Format(body, OurName, OurEmail,
                order.Name + " " + order.Surname, OurMess);
            message.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}