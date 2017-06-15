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
    public class OrderCartDomainModel : BaseDomainModel
    {
        public IEnumerable<OrderCartModel> GetAll()
        {
            var repository3 = new BaseRepository<OrderCart, int>();
            var model_oreder_cart = repository3.Query().Select(x => new Models.OrderCartModel
            {
                Cart_id = x.Cart_id,
                Order_Cart_id = x.Order_Cart_id,
                Order_id = x.Order_id
            }).ToList();
            return model_oreder_cart;
        }
    }
}
