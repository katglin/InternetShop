using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.BLL;

namespace WebShop.Controllers
{
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult Index()
        {
            var domainModel = new BannerDomainModel();
            var domainmodel_products = new ProductDomainModel();
            ViewBag.Products = domainmodel_products.GetAll();
            var image_list = domainModel.GetAll();
            return View(image_list);
        }
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase upload, long artikul)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Images/" + fileName));

                var domainModel = new BannerDomainModel();
                BannerModel bm = new BannerModel();
                bm.Artikul = artikul;
                bm.Image_path = "~/Images/" + fileName;
                bm.Image_id = domainModel.GetAll().Last().Image_id;
                domainModel.Create(bm);
                // удаляем дефолтный рисунок
                var def = domainModel.GetAll().Where(x => x.Image_path == "~/Images/default.png" &&
                            x.Artikul == bm.Artikul);
                if (def != null && def.Count()>0) domainModel.Delete(def.FirstOrDefault().Image_id);
            }
            return RedirectToAction("Index");
        }
    }
}