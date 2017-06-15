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
    public class StaticDomainModel : BaseDomainModel
    {


        public void AddStatic(StaticModel staticmodel)
        {
            using (var repository = new BaseRepository<Static, int>())
            {

                repository.Update(new Static { Static_id = staticmodel.Static_id, About_shop = staticmodel.About_shop,Delivery=staticmodel.Delivery,Sales=staticmodel.Sales });
            }
        }

        public StaticModel GetAll()
        {
            using (var repository = new BaseRepository<Static, int>())
            {
                var list = repository.Query().Select(x => new StaticModel { Static_id = x.Static_id, About_shop = x.About_shop, Delivery = x.Delivery, Sales = x.Sales }).FirstOrDefault();
                return list;
            }
        }

        public Static GetById(int id)
        {
            using (var repository = new BaseRepository<Static, int>())
            {
                return repository.Get(id);
            }
        }

        
    }
}
