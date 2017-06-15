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
    public class CartsDomainModel : BaseDomainModel
    {
        public CartModel Get(long id)
        {
            using (var repository1 = new BaseRepository<DAL.Cart, long>())
            {
                var model = repository1.Query().Where(x => (x.Cart_id == id)).Select(x => new Models.CartModel
                {
                    Cart_id = x.Cart_id,
                    State = x.State,
                    Artikul = x.Artikul,
                    IP = x.IP,
                    Number = x.Number,
                    User_id = x.User_id,
                    date = x.Order_date
                }).FirstOrDefault();
                return model;
            }
        }
        public IEnumerable<CartModel> GetAll()
        {
            using (var repository1 = new BaseRepository<DAL.Cart, long>())
            {
                var model = repository1.Query().Select(x => new Models.CartModel
                {
                    Cart_id = x.Cart_id,
                    State = x.State,
                    Artikul = x.Artikul,
                    IP = x.IP,
                    Number = x.Number,
                    User_id = x.User_id,
                    date = x.Order_date
                }).ToList();
                return model;
            }
        }
        public void Delete_carts(long id)
        {
            var repository2 = new BaseRepository<Order, int>();
            var repository3 = new BaseRepository<OrderCart, int>();
            using (var repository1 = new BaseRepository<DAL.Cart, long>())
            {
                var model = repository1.Query().Where(x => x.Cart_id == id).Select(x => new Models.CartModel
                {
                    Cart_id = x.Cart_id,
                    State = x.State,
                    Artikul = x.Artikul,
                    IP = x.IP,
                    Number = x.Number,
                    User_id = x.User_id,
                    date = x.Order_date
                }).FirstOrDefault();
                repository1.Delete(model.Cart_id);
                var model_oreder_cart = repository3.Query().Select(x => new Models.OrderCartModel
                {
                    Cart_id = model.Cart_id,
                    Order_Cart_id = x.Order_Cart_id,
                    Order_id = x.Order_id
                }).FirstOrDefault();
                repository3.Delete((int)model_oreder_cart.Order_Cart_id);
                var list = repository3.Query().Select(x => new Models.OrderCartModel
                {
                    Cart_id = x.Cart_id,
                    Order_Cart_id = x.Order_Cart_id,
                    Order_id = model_oreder_cart.Order_id
                }).ToList();
                if (list.Count == 0)
                {
                    repository2.Delete((int)model_oreder_cart.Order_id);
                }

            }

        }


        public string Update(CartModel cartmodel, int status_new, DateTime date)
        {
            DAL.Cart up_cart = new DAL.Cart();
            up_cart.Cart_id = cartmodel.Cart_id;
            up_cart.Artikul = cartmodel.Artikul;
            up_cart.IP = cartmodel.IP;
            up_cart.Number = cartmodel.Number;
            up_cart.Order_date = DateTime.Today; //date;
            up_cart.State = status_new;
            up_cart.User_id = cartmodel.User_id;

            using (var repository = new BaseRepository<DAL.Cart, long>())
            {
                repository.Update(up_cart);
                //repository.Delete(cartmodel.Cart_id);
            }
            if (status_new == 3)
            {
                using (var repository1 = new BaseRepository<DAL.Product, long>())
                //using (var hddRep = new BaseRepository<DAL.HardDrive, long>())
                //using (var memRep = new BaseRepository<DAL.Memory, long>())
                //using (var procRep = new BaseRepository<DAL.Processor, long>())
                //using (var videoRep = new BaseRepository<DAL.VideoCard, long>())
                {
                    DAL.Product dalPr = new DAL.Product();
                    dalPr = repository1.Get(cartmodel.Artikul);
                    dalPr.Amount_store = dalPr.Amount_store - cartmodel.Number;
                    dalPr.Popularity++;
                    //var product = repository1.Query().Where(x => (x.Artikul == cartmodel.Artikul)).Select(x => new Models.Product
                    //{
                    //    Amount_store = x.Amount_store,
                    //    Artikul = x.Artikul,
                    //    Cost = x.Cost,
                    //    Information = x.Information,
                    //    Mark = x.Mark,
                    //    Name = x.Name,
                    //    Popularity = x.Popularity,
                    //    Sale = x.Sale,
                    //    Sale_cost = x.Sale_cost,
                    //    Visible = x.Visible
                    //}).FirstOrDefault();
                    //dalPr.Artikul = cartmodel.Artikul;
                    //dalPr.Amount_store = product.Amount_store - cartmodel.Number;
                    //dalPr.Popularity = product.Popularity + 1;
                    //dalPr.Cost = product.Cost;
                    //dalPr.Information = product.Information;
                    //dalPr.Mark = product.Mark;
                    //dalPr.Name = product.Name;
                    //dalPr.Sale = product.Sale;
                    //dalPr.Sale_cost = product.Sale_cost;
                    //dalPr.Visible = product.Visible;
                    //dalPr.HDD = repository1.Get(dalPr.Artikul).HDD;  //hddRep.Get(HDD_id).Size;
                    //dalPr.Memory = repository1.Get(dalPr.Artikul).Memory;  //memRep.Get(Memory_id).Size;
                    //dalPr.Processor = repository1.Get(dalPr.Artikul).Processor;  //procRep.Get(Proc_id).Frequency;
                    //dalPr.VideoCard = repository1.Get(dalPr.Artikul).VideoCard;

                    repository1.Update(dalPr);

                }
            }
            
            return ("Изменения внесены");
        }
        public IEnumerable<CartModel> GetByOrderId(long Order_id)
        {
            List<CartModel> list1=new List<CartModel>();
            using (var repStates = new BaseRepository<OrderState, int>())
            using (var repository = new BaseRepository<OrderCart, int>())
            {
                var list = repository.Query().Select(x => new Models.OrderCartModel
                {
                    Cart_id=x.Cart_id,
                    Order_Cart_id=x.Order_Cart_id,
                    Order_id=x.Order_id
                }).ToList();
                using (var repository1 = new BaseRepository<Cart, int>())
                {
                    foreach (var v in list)
                    {
                        if (v.Order_id == Order_id)
                        {
                            list1.Add(repository1.Query().Where(x=>x.Cart_id==v.Cart_id).Select(x => new Models.CartModel
                            {
                                Cart_id = x.Cart_id,
                                State = x.State,
                                Artikul = x.Artikul,
                                IP = x.IP,
                                Number = x.Number,
                                User_id = x.User_id,
                                date=x.Order_date
                            }).FirstOrDefault());
                        }
                    }
                    
                        }
                        
            }return list1;
        }
        public int CartAmount(bool isAut, string userAut)
        {
            int amount = 0;
            using (var repCart = new BaseRepository<Cart, int>())
            {
                if (isAut)
                {
                    var res = repCart.Query().Where(x => x.User_id == userAut &&
                    x.State == 1);
                    if (res.Count() != 0)
                        amount = res.Sum(x => x.Number); 
                }
                else
                {
                    var res = repCart.Query().Where(x => x.IP == userAut && x.State == 1);
                    if (res.Count() != 0)
                        amount = res.Sum(x => x.Number);
                }
            }
            return amount;
        }

        public IEnumerable<CartHistoryModel> GetHistory(string userID)
        {
            using (var repProduct = new BaseRepository<DAL.Product, int>())
            using (var repStates = new BaseRepository<OrderState, int>())
            using (var repository = new BaseRepository<Cart, int>())
            {
                var list = repository.Query().Select(x => new CartHistoryModel
                    {
                        User_id = x.User_id,
                        Artikul = x.Artikul, 
                        Number = x.Number,
                        Order_date = x.Order_date,
                        State = x.State
                    }).Where(x => x.User_id == userID && x.State != 1).ToList();
                foreach(var item in list)
                {
                    item.Model = repProduct.Get((int)item.Artikul).Mark + " " +
                                   repProduct.Get((int)item.Artikul).Name;
                }

                return list;
            }
        }

        public decimal CalcOrderSum(List <Int64> ids)
        {
            decimal sum = 0;
            using (var repProduct = new BaseRepository<DAL.Product, int>())
            using (var repCart = new BaseRepository<Cart, int>())
            {
                foreach (int id in ids)
                {
                    var cart = repCart.Get(id);
                    var product = repProduct.Get((int)cart.Artikul);
                    sum += (decimal)product.Sale_cost * cart.Number;
                }
            }
            return sum;
        }

        public string[] getShoppingList(List<Int64> ids)
        {
            string[] list = new string[ids.Count];
            using (var repProduct = new BaseRepository<DAL.Product, int>())
            using (var repCart = new BaseRepository<Cart, int>())
            {
                int i = 0;
                foreach (int id in ids)
                {
                    var cart = repCart.Get(id);
                    var product = repProduct.Get((int)cart.Artikul);
                    list[i++] = product.Name + " " + product.Mark + " " + product.Sale_cost +
                        " грн/шт. " + cart.Number + " шт.";
                }
            }
            return list;
        }

        public bool AddOrderForm(OrderFormModel order)
        {
            try
            {
                using (var repStates = new BaseRepository<OrderState, int>())
                using (var repCarts = new BaseRepository<Cart, int>())
                using (var repOrdCarts = new BaseRepository<OrderCart, int>())
                using (var repOrder = new BaseRepository<Order, int>())
                {
                    Order myOrder = new Order();
                    myOrder.Surname = order.Surname;
                    myOrder.Name = order.Name;
                    myOrder.Address = order.Address;
                    myOrder.Phone = order.Phone;
                    myOrder.Email = order.Email;
                    myOrder.Comment = order.Comment;
                    repOrder.Insert(myOrder);
                    long Order_id = repOrder.Query().ToList().Last().Order_id;    
                    foreach (long cart_id in order.Cart_ids)
                    {
                        // add row to Order_Carts
                        OrderCart cart_row = new OrderCart();
                        cart_row.Order_id = myOrder.Order_id;
                        cart_row.Cart_id = cart_id;
                        repOrdCarts.Insert(cart_row);

                        // change state of Carts` rows
                        Cart cart = repCarts.Get((int)cart_id);
                        long state_of_wait = repStates.Query()
                            .Where(x => x.State == "Ожидает отправки").FirstOrDefault().State_id;
                        cart.State = state_of_wait;
                        repCarts.Update(cart);
                    }
                    
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void AddItem(long id, bool isAut, string aut)
        {
            using (var repCart = new BaseRepository<DAL.Cart, int>())
            {
                // maybe we shoud not use 'State = 1', search state_id by state_name instead
                var cart = repCart.Query().Where(x => x.Artikul == id &&
                ((isAut && x.User_id == aut) || (!isAut && x.IP == aut)) &&
                (x.State ==  1 )).FirstOrDefault();
                if (cart == null)
                {
                    Cart cart_row = new Cart();
                    cart_row.Artikul = id;
                    if (isAut)
                    {
                        cart_row.User_id = aut;
                    }
                    else
                    {
                        cart_row.IP = aut;
                    }
                    cart_row.Number = 1;
                    cart_row.State = 1; // look upper
                    repCart.Insert(cart_row);
                }
                else
                {
                    cart.Number++;
                    repCart.Update(cart);
                }               
            }
        }

        public void Save(long cart_id, int number)
        {
            using (var repCart = new BaseRepository<DAL.Cart, int>())
            {
                Cart cart = repCart.Get((int)cart_id);
                cart.Number = number;
                repCart.Update(cart);
            }
        }

        public void Delete(long cart_id)
        {
            using (var repCart = new BaseRepository<DAL.Cart, int>())
            {
                repCart.Delete((int)cart_id);
            }
        }

        public IEnumerable<CartModel> OpenCart(string user) 
        {
            IEnumerable<CartModel> carts;
            using (var repProduct = new BaseRepository<DAL.Product, int>())
            using (var repState = new BaseRepository<DAL.OrderState, int>())
            using (var repCart = new BaseRepository<DAL.Cart, int>())
            {
                carts = repCart.Query().Select(x => new CartModel
                {
                    Buy = false,
                    Cart_id = x.Cart_id,
                    User_id = x.User_id,
                    IP = x.IP,
                    Artikul = x.Artikul,
                    Number = x.Number,
                    //Order_date = x.Order_date,
                    State = x.State,
                }).Where(x => x.User_id == user || x.IP == user).ToList();
                foreach (var cart in carts)
                {
                    cart.StateName = repState.Get((int)cart.State).State;
                    cart.Name = repProduct.Get((int)cart.Artikul).Name;
                    cart.Mark = repProduct.Get((int)cart.Artikul).Mark;
                    cart.Cost = repProduct.Get((int)cart.Artikul).Sale_cost;
                }
                carts = carts.Where(x => x.StateName == "не подтвержден");
            }
            return carts;
        }
    }
}
