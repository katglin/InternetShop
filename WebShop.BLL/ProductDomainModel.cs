using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL;
using WebShop.DAL.Repository;
using WebShop.Models;

namespace WebShop.BLL
{
    public class ProductDomainModel : BaseDomainModel
    {
        public Models.Product GetLast()
        {
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                var list = repository.Query().Select(x => new Models.Product
                {
                    Artikul = x.Artikul,
                    Name = x.Name,
                    Mark = x.Mark,
                    Cost = x.Cost,
                    Sale = x.Sale,
                    Sale_cost = x.Sale_cost,
                    Amount_store = x.Amount_store,
                    Visible = x.Visible,
                    Popularity = x.Popularity
                }).ToList();
                var last = list.Last();
                return last;
            }
        }

        public Models.Product GetOne(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public void Update(Models.ProductPageModel pr, int[] categories, string[] images)
        {
            DAL.Product dalPr = new DAL.Product();
            dalPr.Artikul = pr.Artikul;
            dalPr.Name = pr.Name;
            dalPr.Mark = pr.Mark;
            dalPr.Cost = pr.Cost;
            dalPr.Sale = pr.Sale;
            dalPr.Sale_cost = pr.Sale_cost;
            dalPr.Visible = pr.Visible;
            dalPr.Amount_store = pr.Amount_store;
            dalPr.Information = pr.Information;
            dalPr.Popularity = pr.Popularity;
            dalPr.HDD = pr.HDD_id;
            dalPr.VideoCard = pr.Video_id;
            dalPr.Processor = pr.Proc_id;
            dalPr.Memory = pr.Memory_id;
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                repository.Update(dalPr);
            }
            using (var repository1 = new BaseRepository<DAL.Image, long>())
            {
                    var img = repository1.Query().Where(x=>(x.Artikul==pr.Artikul)).Select(x => new Models.BannerModel
                    {
                        Artikul = x.Artikul,
                        Image_path=x.Image_path,
                        Image_id = x.Image_id
                    }).ToList();
                int count = 0;
                    foreach (var im in images)
                {
                    Image image = new Image();
                    image.Image_id = img[count].Image_id;
                    image.Image_path= im;
                    image.Artikul = img[count].Artikul;
                    repository1.Update(image);
                    count++;
                }
            }
            using (var repository2 = new BaseRepository<DAL.ProductCategory, long>())
            {
                var cats = repository2.Query().Where(x=>x.Artikul==pr.Artikul).Select(x => new ProductCategoryModel
                {
                    Pr_cat_id = x.Pr_cat_id,
                    Category_id = x.Category_id,
                    Artikul = x.Artikul
                }).ToList();
                foreach (var v in cats)
                {
                    repository2.Delete(v.Pr_cat_id);
                }
                foreach (var v in categories)
                {
                    var cat = new ProductCategory();
                    cat.Artikul = dalPr.Artikul;
                    cat.Category_id = v;
                    repository2.Insert(cat);
                }
            }

        }
        //public void Delete(Models.Product pr)
        //{
        //    DAL.Product dalPr = new DAL.Product();
        //    dalPr.Artikul = pr.Artikul;
        //    dalPr.Name = pr.Name;
        //    dalPr.Mark = pr.Mark;
        //    dalPr.Cost = pr.Cost;
        //    dalPr.Sale = pr.Sale;
        //    dalPr.Sale_cost = pr.Sale_cost;
        //    dalPr.Visible = pr.Visible;
        //    dalPr.Amount_store = pr.Amount_store;
        //    dalPr.Popularity = pr.Popularity;
        //    using (var repository = new BaseRepository<DAL.Product, long>())
        //    {
        //        repository.Delete(dalPr.Artikul);
        //    }
        //    using (var repository1 = new BaseRepository<DAL.Image, long>())
        //    {
        //        var images = repository1.Query().Select(x => new Models.BannerModel
        //        {
        //            Artikul = pr.Artikul
        //        }).ToList();
        //        foreach (var im in images)
        //        {
        //            Image Im = new Image();
        //            Im.Artikul = im.Artikul;
        //            Im.Image_id = im.Image_id;
        //            Im.Image_path = im.Image_path;
        //            repository1.Delete(Im.Image_id);
        //        }
        //        //foreach (var pr in list)
        //        //{
        //        //    pr.Image_path = images.Where(c => c.Artikul == pr.Artikul)
        //        //       .Select(c => c.Image_path).FirstOrDefault();
        //        //}
        //    }
        //}

        public Models.Product GetOne(long pr)
        {
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                var list = repository.Query().Select(x => new Models.Product
                {
                    Artikul = x.Artikul,
                    Name = x.Name,
                    Mark = x.Mark,
                    Cost = x.Cost,
                    Sale = x.Sale,
                    Sale_cost = x.Sale_cost,
                    Amount_store = x.Amount_store,
                    Visible = x.Visible,
                    Popularity = x.Popularity
                }).ToList();

                var product = list.FirstOrDefault(g => g.Artikul == pr);
                return product;
            }
        }

        public void Create(Models.ProductPageModel pr, int[] categories)
        {
            DAL.Product dalPr = new DAL.Product();
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                dalPr.Name = pr.Name;
                dalPr.Mark = pr.Mark;
                dalPr.Cost = pr.Cost;
                dalPr.Sale = pr.Sale;
                dalPr.Sale_cost = pr.Sale_cost;
                dalPr.Visible = pr.Visible;
                dalPr.Amount_store = pr.Amount_store;
                dalPr.Information = pr.Information;
                dalPr.Popularity = pr.Popularity;
                dalPr.HDD = pr.HDD_id;
                dalPr.VideoCard = pr.Video_id;
                dalPr.Processor = pr.Proc_id;
                dalPr.Memory = pr.Memory_id;
                var list = repository.Query().Select(x => new Models.Product
                {
                    Artikul = x.Artikul
                }).ToList();
                var last = list.Last();
                dalPr.Artikul = last.Artikul;
                repository.Insert(dalPr);
            }
                using (var repository1 = new BaseRepository<DAL.Image, long>())
                {
                    var images = repository1.Query().Select(x => new Models.BannerModel
                    {
                        Image_id = x.Image_id,
                        Image_path = x.Image_path,
                        Artikul = x.Artikul
                    }).ToList();
                    var img = new Image();
                    img.Artikul = dalPr.Artikul;
                    img.Image_id = images.Last().Image_id;
                    img.Image_path = "~/Images/default.png"/*pr.img.FirstOrDefault()*/;
                    repository1.Insert(img);
                    //foreach (var pr in list)
                    //{
                    //    pr.Image_path = images.Where(c => c.Artikul == pr.Artikul)
                    //       .Select(c => c.Image_path).FirstOrDefault();
                    //}
                }
            using (var repository2 = new BaseRepository<DAL.ProductCategory, long>())
            {
                foreach (var v in categories)
                {
                    //var cats = repository2.Query().Select(x => new DAL.ProductCategory
                    //{
                    //    Pr_cat_id = x.Pr_cat_id,
                    //    Category_id = x.Category_id,
                    //    Artikul = x.Artikul
                    //}).ToList();
                    var cat = new ProductCategory();
                    cat.Artikul = dalPr.Artikul;
                    cat.Category_id = v;
                    //cat.Pr_cat_id = cats.Last().Pr_cat_id;
                    repository2.Insert(cat);
                }
                //foreach (var pr in list)
                //{
                //    pr.Image_path = images.Where(c => c.Artikul == pr.Artikul)
                //       .Select(c => c.Image_path).FirstOrDefault();
                //}
            }
            // repository.Insert(new ProductTable { Name = "Test1" });
        }
        

        public IEnumerable<Models.Product> GetAll()
        {
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                var list = repository.Query().Select(x => new Models.Product
                {
                    Artikul = x.Artikul,
                    Name = x.Name,
                    Mark = x.Mark,
                    Cost = x.Cost,
                    Sale = x.Sale,
                    Sale_cost = x.Sale_cost,
                    Amount_store = x.Amount_store,
                    Visible = x.Visible,
                    Popularity = x.Popularity
                }).ToList();
                return list;
            }
        }

        // Take product description for Page "ProductsList" from 2 tables:
        // Image_path + all Product fields
        public IEnumerable<Models.ProductInfoModel> GetProductInfo()
        {
            List<Models.ProductInfoModel> list = new List<Models.ProductInfoModel>();//
            using (var hddRep = new BaseRepository<DAL.HardDrive, long>())
            using (var memRep = new BaseRepository<DAL.Memory, long>())
            using (var procRep = new BaseRepository<DAL.Processor, long>())
            using (var videoRep = new BaseRepository<DAL.VideoCard, long>())
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                var products = repository.Query().Where(x=> x.Visible==true).Select(x => new 
                {
                    Artikul = x.Artikul,
                    Name = x.Name,
                    Mark = x.Mark,
                    Cost = x.Cost,
                    Sale = x.Sale,
                    Sale_cost = x.Sale_cost,
                    Amount_store = x.Amount_store,
                    Visible = x.Visible,
                    Popularity = x.Popularity,
                    HddId = x.HDD,
                    VideoId = x.VideoCard,
                    MemId = x.Memory,
                    ProcId = x.Processor
                }).ToList();
                foreach (var pr in products)
                {
                    var p = new Models.ProductInfoModel();
                    p.Artikul = pr.Artikul;
                    p.Name = pr.Name;
                    p.Mark = pr.Mark;
                    p.Cost = pr.Cost;
                    p.Sale = pr.Sale;
                    p.Sale_cost = pr.Sale_cost;
                    p.Amount_store = pr.Amount_store;
                    p.Visible = pr.Visible;
                    p.Popularity = pr.Popularity;

                    p.HddSize = hddRep.Query().Where(x => x.Hdd_id == pr.HddId).FirstOrDefault().Size;
                    p.PhysMem = memRep.Query().Where(x=> x.Memory_id==pr.MemId).FirstOrDefault().Size;
                    p.ProcName = procRep.Query().Where(x=>x.Processor_id==pr.ProcId).FirstOrDefault().Producer;
                    p.ProcFreq = procRep.Query().Where(x => x.Processor_id == pr.ProcId).FirstOrDefault().Frequency;

                    p.VideoCard = videoRep.Query().Where(x=>x.VideoCard_id==pr.VideoId).FirstOrDefault().Producer;
                    p.VideoMem = videoRep.Query().Where(x => x.VideoCard_id == pr.VideoId).FirstOrDefault().Memory;
                    list.Add(p);
                }
            }
            using (var repository = new BaseRepository<DAL.Image, long>())
            {
                var images = repository.Query().Select(x => new Models.BannerModel
                {
                    Image_id = x.Image_id,
                    Image_path = x.Image_path,
                    Artikul = x.Artikul
                }).ToList();

                foreach (var pr in list)
                {
                    pr.Image_path = images.Where(c => c.Artikul == pr.Artikul)
                       .Select(c => c.Image_path).FirstOrDefault();
                }
            }
            return list;
        }

        public IEnumerable<Models.ProductInfoModel> GetProductInfoByCategory(int categId)
        {
            List<Models.ProductInfoModel> products = new List<Models.ProductInfoModel>();
            List<long> ids = new List<long>();

            using (var repository = new BaseRepository<DAL.ProductCategory, long>())
            {
                // List of Artikuls of category 'categId'
                var prIds = repository.Query().Where(x => x.Category_id == categId).Select(x => new { artikul = x.Artikul }).ToList();
                ids = prIds.Select(x => x.artikul).ToList();
            }
            using (var hddRep = new BaseRepository<DAL.HardDrive, long>())
            using (var memRep = new BaseRepository<DAL.Memory, long>())
            using (var procRep = new BaseRepository<DAL.Processor, long>())
            using (var videoRep = new BaseRepository<DAL.VideoCard, long>())
            using (var repository = new BaseRepository<DAL.Product, long>())
            {
                foreach (long id in ids)
                {
                    var pr = repository.Query().Where(x => x.Visible == true && x.Artikul==id).Select(x => new
                    {
                        Artikul = x.Artikul,
                        Name = x.Name,
                        Mark = x.Mark,
                        Cost = x.Cost,
                        Sale = x.Sale,
                        Sale_cost = x.Sale_cost,
                        Amount_store = x.Amount_store,
                        Visible = x.Visible,
                        Popularity = x.Popularity,
                        HddId = x.HDD,
                        VideoId = x.VideoCard,
                        MemId = x.Memory,
                        ProcId = x.Processor
                    }).FirstOrDefault();
                    var p = new Models.ProductInfoModel();
                    p.Artikul = pr.Artikul;
                    p.Name = pr.Name;
                    p.Mark = pr.Mark;
                    p.Cost = pr.Cost;
                    p.Sale = pr.Sale;
                    p.Sale_cost = pr.Sale_cost;
                    p.Amount_store = pr.Amount_store;
                    p.Visible = pr.Visible;
                    p.Popularity = pr.Popularity;

                    p.HddSize = hddRep.Query().Where(x => x.Hdd_id == pr.HddId).FirstOrDefault().Size;
                    p.PhysMem = memRep.Query().Where(x => x.Memory_id == pr.MemId).FirstOrDefault().Size;
                    p.ProcName = procRep.Query().Where(x => x.Processor_id == pr.ProcId).FirstOrDefault().Producer;
                    p.ProcFreq = procRep.Query().Where(x => x.Processor_id == pr.ProcId).FirstOrDefault().Frequency;

                    p.VideoCard = videoRep.Query().Where(x => x.VideoCard_id == pr.VideoId).FirstOrDefault().Producer;
                    p.VideoMem = videoRep.Query().Where(x => x.VideoCard_id == pr.VideoId).FirstOrDefault().Memory;
                    products.Add(p);
                }
            }

            using (var repository = new BaseRepository<DAL.Image, long>())
            {
                var images = repository.Query().Select(x => new Models.BannerModel
                {
                    Image_id = x.Image_id,
                    Image_path = x.Image_path,
                    Artikul = x.Artikul
                }).ToList();

                foreach (var pr in products)
                {
                    pr.Image_path = images.Where(c => c.Artikul == pr.Artikul).Select(c => c.Image_path).FirstOrDefault();
                }
            }

            return products;
        }         

        public Models.ProductPageModel ProductPage(long id)
        {
            ProductPageModel result = new ProductPageModel();
            using (var hddRep = new BaseRepository<DAL.HardDrive, long>())
            using (var memRep = new BaseRepository<DAL.Memory, long>())
            using (var procRep = new BaseRepository<DAL.Processor, long>())
            using (var videoRep = new BaseRepository<DAL.VideoCard, long>())
            using (var repository = new BaseRepository<DAL.Product, long>())
            {

                var product = repository.Query().Where(x => x.Artikul == id).Select(x => new Models.ProductPageModel
                {
                    Artikul = x.Artikul,
                    Name = x.Name,
                    Mark = x.Mark,
                    Cost = x.Cost,
                    Sale = x.Sale,
                    Sale_cost = x.Sale_cost,
                    Amount_store = x.Amount_store,
                    Visible = x.Visible,
                    Information = x.Information,
                    Popularity = x.Popularity,
                    HDD_id = x.HDD,
                    Proc_id = x.Processor,
                    Memory_id = x.Memory,
                    Video_id = x.VideoCard
                }).FirstOrDefault();
                product.HDD = hddRep.Query().Where(x => x.Hdd_id == product.HDD_id).FirstOrDefault().Size;
                product.Memory = memRep.Query().Where(x => x.Memory_id == product.Memory_id).FirstOrDefault().Size;
                product.ProcessorFreq = procRep.Query().Where(x => x.Processor_id == product.Proc_id)
                            .FirstOrDefault().Frequency;
                product.ProcessorProducer = procRep.Query().Where(x => x.Processor_id == product.Proc_id)
                            .FirstOrDefault().Producer;
                product.VideoMemory = videoRep.Query().Where(x => x.VideoCard_id == product.Video_id)
                            .FirstOrDefault().Memory;
                product.VideoProducer = videoRep.Query().Where(x => x.VideoCard_id == product.Video_id)
                            .FirstOrDefault().Producer;

                result = product;
            }

            using (var repository = new BaseRepository<DAL.Image, long>())
            {
                IEnumerable<Models.ProductInfoModel> list;
                var images = repository.Query().Where(x => x.Artikul == id).Select(x => new Models.BannerModel
                {
                    Image_id = x.Image_id,
                    Image_path = x.Image_path,
                    Artikul = x.Artikul
                }).ToList();
                bool find = false;
                int i = 0;
                foreach (var im in images)
                {
                    if (im.Artikul == id)
                    {
                        result.img.Add(im.Image_path);
                        i++;
                    }
                    if (i == 4) break;
                }
            }
            return result;
        }

        public List<CommentModel> CommentsForProduct(long artikul)
        {
            List <CommentModel> comments;
            using (var repository = new BaseRepository<DAL.Comment, long>())
            {
                comments = repository.Query().Select(x => new CommentModel
                {
                    Comment_id = x.Comment_id,
                    User_name = x.User_id,
                    Artikul = x.Artikul,
                    Comment_text = x.Comment_text,
                    Comment_date = x.Comment_date
                }).Where(x => x.Artikul == artikul).OrderByDescending(x => x.Comment_date).ToList();
            }
            return comments;
        }
        
        public ProductPageWithComments getPageWithComments(long artikul)
        {
            ProductPageWithComments page = new ProductPageWithComments();
            page.product = ProductPage(artikul);
            page.comments = CommentsForProduct(artikul);
            using (var repository = new BaseRepository<DAL.PrCategory, long>())
            {
                int count = 5;

                var id = repository.Query().Where(x => x.Category == "Рекомедуемые").FirstOrDefault().Category_id;
                page.similarProducts = GetProductInfoByCategory((int)id);
                if (page.similarProducts.Count() >= count)
                {
                    page.similarProducts = page.similarProducts.Take(count);
                }
            }
            page.recentProducts = new Queue<ProductInfoModel>();
            using (var repImage = new BaseRepository<Image, long>())
            using (var repository = new BaseRepository<DAL.Product, long>())
            foreach (var id in (Queue<long>)System.Web.HttpContext.Current.Session["prev"])
            {
                var product = repository.Get(id);
                    page.recentProducts.Enqueue(new ProductInfoModel
                    {
                        Artikul = product.Artikul,
                        Name = product.Name,
                        Mark = product.Mark,
                        Cost = product.Cost,
                        Sale = product.Sale,
                        Sale_cost = product.Sale_cost,
                        Amount_store = product.Amount_store,
                        Visible = product.Visible,
                        Information = product.Information,
                        Popularity = product.Popularity,
                        Image_path = ""
                });
                    try
                    {
                        page.recentProducts.Last().Image_path = repImage.Query().Where(x => x.Artikul == product.Artikul).FirstOrDefault().Image_path;
                    }
                    catch
                    {
                        page.recentProducts.Last().Image_path = "~/Images/default.png";
                    }
                }


            return page;
        }

        public void AddComment(long artikul, string comment, string user)
        {
            DateTime thisTime = DateTime.Now;
            TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            DateTime _localTime = TimeZoneInfo.ConvertTime(thisTime, TimeZoneInfo.Local, myTZ);

            //DateTime serverTime = DateTime.Now;
            //DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            //    serverTime, "Mountain Standard Time", TimeZoneInfo.Local.Id);
            using (var repository = new BaseRepository<DAL.Comment, long>())
            {
                Comment com = new Comment();
                com.Artikul = artikul;
                com.Comment_date = _localTime;
                com.Comment_text = comment;
                com.User_id = user;
                repository.Insert(com);
            }
        }

        public List<HddModel> GetHdds()
        {
            using (var rep = new BaseRepository<DAL.HardDrive, long>())
            {
                List<HddModel> hdds = rep.Query().Select(x => new HddModel
                {
                    HDD_id = x.Hdd_id,
                    HDD = x.Size
                }).ToList();
                return hdds;
            }
        }

        public List<VideoCardModel> GetVCs()
        {
            using (var rep = new BaseRepository<DAL.VideoCard, long>())
            {
                List<VideoCardModel> vcs = rep.Query().Select(x => new VideoCardModel
                {
                    Video_id = x.VideoCard_id,
                //    VideoMemory = x.Memory,
                    VideoProducer = x.Producer + " " + x.Memory + " ГБ"
                }).ToList();
                return vcs;
            }
        }

        public List<ProcModel> GetPrs()
        {
            using (var rep = new BaseRepository<DAL.Processor, long>())
            {
                List<ProcModel> prs = rep.Query().Select(x => new ProcModel
                {
                    Proc_id = x.Processor_id,
                  //  ProcessorFreq = x.Frequency,
                    ProcessorProducer = x.Producer+" "+x.Frequency + " GHz"
                }).ToList();
                return prs;
            }
        }

        public List<MemoryModel> GetMems()
        {
            using (var rep = new BaseRepository<DAL.Memory, long>())
            {
                List<MemoryModel> mems = rep.Query().Select(x => new MemoryModel
                {
                    Memory_id = x.Memory_id,
                    Memory = x.Size
                }).ToList();
                return mems;
            }
        }

    }
}
