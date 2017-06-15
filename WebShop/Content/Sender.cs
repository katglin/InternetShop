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
namespace WebShop.Content
{
    class First
    {
        public bool SendEmailaboutStatus(OrderModel order, string message_about_status)
        {
            //string[] shoppingList;
            //using (var domainModel = new CartsDomainModel())
            //{
            //    order.OrderSum = domainModel.CalcOrderSum(order.Cart_ids);
            //    shoppingList = domainModel.getShoppingList(order.Cart_ids);
            //}
            string OurName = "Интернет-магазин 'ТелеМаг'";
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
            message.Subject = "Оформление заказа в Интеренет-магазине 'ТелеМаг'";
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