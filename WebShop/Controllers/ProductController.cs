using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.BLL;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Example
        public ActionResult Index()
        {
            using (var domainModel = new ProductDomainModel())
            {
                var model = domainModel.GetAll();
                return View(model);
            }
        }

        public ActionResult AddOne(ProductPageModel productPM, int[] categories)
        {
            using (var domainModel = new ProductDomainModel())
            {           
                domainModel.Create(productPM, categories);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Changing(long artikul)
        {
            var domainModel = new ProductDomainModel();
            var domainModel1 = new PrCategoryDomainModel();
            Product product = domainModel.GetOne(artikul);
            ProductPageModel PPM = domainModel.ProductPage(artikul);
            ViewBag.Categories = domainModel1.GetAll();
            ViewBag.Hdds = domainModel.GetHdds();
            ViewBag.Videocards = domainModel.GetVCs();
            ViewBag.Mems = domainModel.GetMems();
            ViewBag.Processors = domainModel.GetPrs();
            ViewBag.Categories_FP = domainModel1.GetAll_ForProduct(product);
            return View(PPM);
        }
        [HttpPost]
        public ActionResult Add_Changes(ProductPageModel product, int[] categories, string[] img)
        {
            var domainModel = new ProductDomainModel();
            domainModel.Update(product, categories, img);
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(long artikul)
        //{
        //    var domainModel = new ProductDomainModel();
        //    Product product = domainModel.GetOne(artikul);
        //    domainModel.Delete(product);
        //    return RedirectToAction("Index");
        //}
        public ActionResult Adding()
        {
            var domainModel = new ProductDomainModel();
            var domainModel1 = new PrCategoryDomainModel();
            ProductPageModel product = new ProductPageModel();
            product.img.Add("~/Images/default.png");
            ViewBag.Categories = domainModel1.GetAll();

            ViewBag.Hdds = domainModel.GetHdds();
            ViewBag.Videocards = domainModel.GetVCs();
            ViewBag.Mems = domainModel.GetMems();
            ViewBag.Processors = domainModel.GetPrs();

            return View(product);
        }
        [HttpPost]
        public ActionResult Search(string SearchingString)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var domainModel = new ProductDomainModel();
            var model = domainModel.GetAll().Where(x => x.Name.Contains(SearchingString));
            return View("Index", model);
        }
        [HttpPost]
        public double Calculate(double cost, double sale)
        {
            if ((cost >= 0) && (cost <= 100000)&&(sale>=0)&&(sale<=100))
            {
                return (cost - (double)((cost * sale) / 100));
            }
            else { return 0; }
        }
    }
}