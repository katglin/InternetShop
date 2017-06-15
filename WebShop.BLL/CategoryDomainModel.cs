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
    public class CategoryDomainModel : BaseDomainModel
    {
        public IEnumerable<ProductCategory> GetAll(Models.Product p)
        {
            using (var repository = new BaseRepository<ProductCategory, int>())
            {
                DAL.Product p1 = new DAL.Product();
                p1.Artikul = p.Artikul;
                var list = repository.Query().Select(x => new ProductCategory
                { Category_id = x.Category_id, Artikul = x.Artikul, Pr_cat_id=x.Pr_cat_id }).ToList();
                var list1 = list.Where(n=>n.Artikul==p1.Artikul);
                return list;
            }
        }
    }
}
