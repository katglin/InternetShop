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
    public class OrderDomainModel : BaseDomainModel
    {
        public IEnumerable<Models.OrderModel> GetAll()
        {
            using (var repository = new BaseRepository<DAL.Order, long>())
            {
                var list = repository.Query().Select(x => new Models.OrderModel
                {
                    Order_id=x.Order_id,
                    Surname = x.Surname,
                    Name = x.Name,
                    Address = x.Address,
                    Email = x.Email,
                    Phone = x.Phone,
                    Comment = x.Comment
                }).ToList();
                using (var cartsdm = new CartsDomainModel())
                foreach (OrderModel ord in list)
                {
                    ord.Attent = false;
                    foreach (var rec in cartsdm.GetByOrderId(ord.Order_id))
                    {
                        if(rec.State==2 || rec.State==4)
                            {
                                ord.Attent = true;
                                break;
                            }
                    }
                }
                return list;
            }
        }
        public void Delete(long order_id)
        {
            using (var repository = new BaseRepository<DAL.Order, long>())
            {
                var list = repository.Query().Select(x => new Models.OrderModel
                {
                    Order_id = order_id,
                    Surname = x.Surname,
                    Name = x.Name,
                    Address = x.Address,
                    Email = x.Email,
                    Phone = x.Phone,
                    Comment = x.Comment
                }).FirstOrDefault();
                repository.Delete(list.Order_id);
            }
        }
        public Models.OrderModel GetOne(long cart_id)
        {
            OrderModel list1 = new OrderModel();
            using (var repStates = new BaseRepository<OrderState, int>())
            using (var repository = new BaseRepository<OrderCart, int>())
            {
                var list = repository.Query().Where(x=>x.Cart_id==cart_id).Select(x => new Models.OrderCartModel
                {
                    Cart_id = x.Cart_id,
                    Order_Cart_id = x.Order_Cart_id,
                    Order_id = x.Order_id
                }).FirstOrDefault();
                var repository1 = new BaseRepository<Order, int>();


                list1 = repository1.Query().Where(x => x.Order_id == list.Order_id).Select(x => new Models.OrderModel
                {
                    Address = x.Address,
                    Comment = x.Comment,
                    Email=x.Email,
                    Name=x.Name,
                    Order_id=x.Order_id,
                    Phone=x.Phone,
                    Surname=x.Surname
                            }).FirstOrDefault();
                        

                

            }
            return list1;
        }
    }
}
