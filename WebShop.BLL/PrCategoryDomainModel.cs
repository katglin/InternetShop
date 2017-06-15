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
    public class PrCategoryDomainModel : BaseDomainModel
    {
        public IEnumerable<PrCategoryModel> GetAll()
        {
            using (var repository = new BaseRepository<PrCategory, int>())
            {
                var list = repository.Query().Select(x => new PrCategoryModel
                            { Category_id = x.Category_id, Category = x.Category }).ToList();
                return list;
            }
        }
        public IEnumerable<PrCategoryModel> GetAll_ForProduct(Models.Product pr)
        {
            using (var repository = new BaseRepository<ProductCategory, int>())
            {
                //foreach(var cat in pr.Categories)
                var list = repository.Query().Where(x=>x.Artikul==pr.Artikul).Select(x => new ProductCategoryModel
                { Category_id = x.Category_id, Artikul = x.Artikul, Pr_cat_id=x.Pr_cat_id }).ToList();
            
            using (var repository1 = new BaseRepository<PrCategory, int>())
            {
                    List<PrCategoryModel> list1 = new List<PrCategoryModel>();
                foreach (var param in list)
                {
                    list1.Add(repository1.Query().Where(x=>x.Category_id==param.Category_id).Select(x => new PrCategoryModel
                    { Category_id = x.Category_id, Category = x.Category }).FirstOrDefault());
                }
                    return list1;
                }
                
            }
                
            }
        }
    }

