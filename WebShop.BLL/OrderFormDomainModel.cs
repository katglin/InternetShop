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
    public class OrderFormDomainModel : BaseDomainModel
    {
        public IEnumerable<Models.OrderFormModel> GetAll()
        {
            using (var repository = new BaseRepository<DAL.Order, long>())
            {
                var list = repository.Query().Select(x => new Models.OrderFormModel
                {
                    Surname=x.Surname,
                    Name=x.Name,
                    Address=x.Address,
                    Email=x.Email,
                    Phone=x.Phone,
                    Comment=x.Comment                  
                }).ToList();
                return list;
            }
        }
    }

}
