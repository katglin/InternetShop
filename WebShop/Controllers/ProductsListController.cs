using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.BLL;
using WebShop.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;

namespace WebShop.Controllers
{
    public class ProductsListController : Controller
    {
        List<ProductInfoModel> list = null;
        Queue<long> prev = new Queue<long>();
        bool isCurrent = true;
        // GET: Products
        public ActionResult Index(int? page, string sortOrder, int? categId, string SearchString,
            string amount, string hdd, string sales, string currentFilter, string minPr, 
            string maxPr, string present, string minHDD, string maxHDD, string freq, string minFreq, 
            string maxFreq, string mem, string maxMem, string minMem, 
            string asus, string acer, string lenovo, string dell, string hp, string other)
        {
            (new CartsController()).CartAmount();
            if (categId != null)
            {
                ViewBag.CategoryItem = (int)categId;
                ViewBag.Category = (int)categId;
            }

            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;

            using (var domainModel = new ProductDomainModel())
                {
                
                if (list == null && categId == null)
                    list = domainModel.GetProductInfo().ToList();
                else if (list == null && categId != null)
                     list = domainModel.GetProductInfoByCategory((int)categId).ToList();
              
  
                if(list!=null && list.Count > 0)
                {
                    ViewBag.MaxPrice = list.Max(x => x.Sale_cost);
                    ViewBag.MaxHDDSize = list.Max(x=> x.HddSize);
                    ViewBag.MaxMemory = list.Max(x => x.PhysMem);

                    if (!String.IsNullOrEmpty(SearchString))
                    {
                        SearchString = SearchString.ToUpper();
                        list = list.Where(x => x.Name.ToUpper().Contains(SearchString)
                                        || x.Mark.ToUpper().Contains(SearchString)
                                        || x.Cost.ToString().ToUpper().Contains(SearchString)
                                || x.Sale_cost.ToString().ToUpper().Contains(SearchString)).ToList();
                    }
                    if (!String.IsNullOrEmpty(sales))
                    {
                        ViewBag.SalesCheck = "checked";
                        list = list.Where(x => x.Sale > 0).ToList();
                    }
                    if (!String.IsNullOrEmpty(present))
                    {
                        ViewBag.PresentCheck = "checked";
                        list = list.Where(x => x.Amount_store > 0).ToList();
                    }
                    if (!String.IsNullOrEmpty(hdd))
                    {
                        string[] hdds = hdd.Split('-');
                        double min = Convert.ToDouble(hdds[0]);
                        double max = Convert.ToDouble(hdds[1]);
                        list = list.Where(x => x.HddSize >= Convert.ToDouble(min) &&
                                x.HddSize <= Convert.ToDouble(max)).ToList();
                        ViewBag.minHDD = min.ToString();
                        ViewBag.maxHDD = max.ToString();
                    }
                    else if (maxHDD != null || minHDD != null)
                    {
                        ViewBag.maxHDD = ViewBag.MaxHDDSize;
                        ViewBag.minHDD = ViewBag.MinHDDSize;
                        if (minHDD != null)  ViewBag.minHDD = minHDD;
                        if (maxHDD != null) ViewBag.maxHDD = maxHDD;
                        list = list.Where(x => x.HddSize >= Convert.ToDouble(minHDD) &&
                                x.HddSize <= Convert.ToDouble(maxHDD)).ToList();
                    }
                    else
                    {
                        ViewBag.maxHDD = ViewBag.MaxHDDSize;
                        ViewBag.minHDD = ViewBag.MinHDDSize;
                    }
                    if (!String.IsNullOrEmpty(freq))
                    {
                        string[] freqs = freq.Split('-');
                        double min = Convert.ToDouble(freqs[0]);
                        double max = Convert.ToDouble(freqs[1]);
                        list = list.Where(x => x.ProcFreq >= min && x.ProcFreq <= max).ToList();
                        ViewBag.minFreq = min;// min.ToString();
                        ViewBag.maxFreq = max; //.ToString();
                    }
                    else
                    {
                        ViewBag.minFreq = 0; ViewBag.maxFreq = 10;
                        if (minFreq != null) ViewBag.minFreq = minFreq;
                        if (maxFreq != null) ViewBag.maxFreq = maxFreq;
                        list = list.Where(x => x.ProcFreq >= Convert.ToDouble(ViewBag.minFreq) &&
                                x.ProcFreq <= Convert.ToDouble(ViewBag.maxFreq)).ToList();
                    }
                    if (!String.IsNullOrEmpty(mem))
                    {
                        string[] mems = mem.Split('-');
                        double min = Convert.ToDouble(mems[0]);
                        double max = Convert.ToDouble(mems[1]);
                        list = list.Where(x => x.PhysMem >= min && x.PhysMem <= max).ToList();
                        ViewBag.minMem = min;// min.ToString();
                        ViewBag.maxMem = max; //.ToString();
                    }
                    else
                    {
                        ViewBag.minMem = 0; ViewBag.maxMem = ViewBag.MaxMemory;
                        if (minMem != null) ViewBag.minMem = minMem;
                        if (maxMem != null) ViewBag.maxMem = maxMem;
                        list = list.Where(x => x.PhysMem >= Convert.ToDouble(ViewBag.minMem) &&
                                x.PhysMem <= Convert.ToDouble(ViewBag.maxMem)).ToList();
                    }
                    if (!String.IsNullOrEmpty(amount))
                    {
                        string[] amounts = amount.Split('-');
                        double min = Convert.ToDouble(amounts[0]);
                        double max = Convert.ToDouble(amounts[1]);
                        list = list.Where(x => x.Sale_cost >= min && x.Sale_cost <= max).ToList();
                        ViewBag.min = min.ToString();
                        ViewBag.max = max.ToString();
                    }
                    else if (maxPr != null)
                    {
                        ViewBag.min = minPr;
                        ViewBag.max = maxPr;
                        list = list.Where(x => x.Sale_cost >= Convert.ToDouble(minPr) &&
                                x.Sale_cost <= Convert.ToDouble(maxPr)).ToList();
                    }
                    else
                    {
                        ViewBag.max = ViewBag.MaxPrice;
                    }
             //       if (list != null && list.Count > 0)
             //       {
                        bool oth = false;
                        List<string> nb = new List<string>();
                        if (!String.IsNullOrEmpty(asus)) { nb.Add("Asus"); ViewBag.asus = "checked"; }
                        if (!String.IsNullOrEmpty(acer)) { nb.Add("Acer"); ViewBag.acer = "checked"; }
                        if (!String.IsNullOrEmpty(dell)) { nb.Add("Dell"); ViewBag.dell = "checked"; }
                        if (!String.IsNullOrEmpty(hp)) { nb.Add("HP"); ViewBag.hp = "checked"; }
                        if (!String.IsNullOrEmpty(lenovo)) { nb.Add("Lenovo"); ViewBag.lenovo = "checked"; }
                        if (!String.IsNullOrEmpty(other)) { oth = true; ViewBag.other = "checked"; }

                        if (nb.Count > 0 && !oth) list = list.Where(x => nb.Contains(x.Mark)).ToList();
                        else if (oth)
                        {
                            if (!nb.Contains("Asus")) list = list.Where(x => x.Mark != "Asus").ToList();
                            if (!nb.Contains("Acer")) list = list.Where(x => x.Mark != "Acer").ToList();
                            if (!nb.Contains("Dell")) list = list.Where(x => x.Mark != "Dell").ToList();
                            if (!nb.Contains("HP")) list = list.Where(x => x.Mark != "HP").ToList();
                            if (!nb.Contains("Lenovo")) list = list.Where(x => x.Mark != "Lenovo").ToList();
                        }
                        else
                        {
                            ViewBag.asus = "checked";
                            ViewBag.acer = "checked";
                            ViewBag.dell = "checked";
                            ViewBag.hp = "checked";
                            ViewBag.lenovo = "checked";
                            ViewBag.other = "checked";
                        }
                //    }


                    ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) || sortOrder == "Name"
                                            ? "Name desc" : "Name";
                    ViewBag.CostSortParam = sortOrder == "Cost" ? "Cost desc" : "Cost";
                    ViewBag.PopulSortParam = sortOrder == "Popul" ? "Popul desc" : "Popul desc";

                    switch (sortOrder)
                    {
                        case "Name desc":
                            list = list.OrderByDescending(s => s.Name).ToList();
                            break;
                        case "Cost":
                            list = list.OrderBy(s => s.Sale_cost).ToList();
                            break;
                        case "Cost desc":
                            list = list.OrderByDescending(s => s.Sale_cost).ToList();
                            break;
                        case "Popul desc":
                            list = list.OrderByDescending(s => s.Popularity).ToList();
                            break;
                        default:
                            list = list.OrderBy(s => s.Name).ToList();
                            break;
                    }
                    if (String.IsNullOrEmpty(sortOrder)) ViewBag.sortValue = "Name";
                    else ViewBag.sortValue = sortOrder;
                }     
                ViewBag.MenuItem = "Products";

                if(list!=null && list.Count>0)
                {
                    var topMinPopul = list.OrderByDescending(x => x.Popularity).Take(10).Min(x => x.Popularity);
                    Session["topMinPopul"] = topMinPopul;
                }
                int pageSize = 15;
                int pageNumber = (page ?? 1);

                return View(list.ToPagedList(pageNumber, pageSize));
            }
        }

        public ActionResult Page(int? id, int? curPageNumber)
        {
            long prevElement = 0;
            if (Session["prevId"] == null)
            {
                Session["prevId"] = new Int64();
            }
            if (id != null)
            {
                if ((long)Session["prevId"] != id)
                {
                    prevElement = (long)Session["prevId"];
                    Session["prevId"] = (long)id;
                    isCurrent = false; 
                }
                else
                {
                    isCurrent = true;
                }

                if (Session["prev"] == null)
                    {
                        Session["prev"] = new Queue<long>();
                    }
                else
                {
                        prev = (Queue<long>)Session["prev"];
                        if (!isCurrent && !prev.Contains(prevElement) && prevElement != 0)
                        {
                            prev.Enqueue(prevElement);
                        }
                        while (prev.Count > 5)
                            prev.Dequeue();
                        Session["prev"] = prev;
                }
                if (curPageNumber != null) ViewBag.ScrollToAnchor = true;
                else ViewBag.ScrollToAnchor = false;
                using (var domainModel = new ProductDomainModel())
                {
                    ProductPageWithComments page = domainModel.getPageWithComments((int)id);
                    ViewBag.CommentAmount = page.comments.Count;
                    var db = new ApplicationDbContext();
                    var users = db.Users.ToList();
                    foreach (var com in page.comments)
                    {
                        var user = users.Where(x => x.Id == com.User_name).FirstOrDefault();
                        com.User_name = user.Name + " " + user.Surname;
                    }
                    int pageSize = 5; // количество объектов на страницу
                    if (curPageNumber == null)
                    {
                        curPageNumber = 1;
                    }
                    PageInfo pageInfo = new PageInfo { PageNumber = (int)curPageNumber,
                                PageSize = pageSize, TotalItems = page.comments.Count };
                    page.PageInfo = pageInfo;
                    page.comments = page.comments.Skip(((int)curPageNumber - 1) * pageSize).Take(pageSize).ToList();
                    return View(page);
                }
            }
            else return View("Index");
        }       

        [HttpPost]
        public ActionResult Page(int? id, string comment)
        {
                          
            if(!String.IsNullOrEmpty(comment))
            {
                using (var domModel = new ProductDomainModel())
                {
                    domModel.AddComment((int)id, comment, HttpContext.User.Identity.GetUserId());
                }
            }
            return RedirectToAction("Page", new { id = id, curPageNumber  = 1});
        }
    }
}