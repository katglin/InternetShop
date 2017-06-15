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
    public class ExampleDomainModel : BaseDomainModel
    {
        public void AddExample()
        {
            using (var repository = new BaseRepository<ExampleTable, int>())
            {
                repository.Insert(new ExampleTable { Title = "Test1" });
            }
        }

        public IEnumerable<Example> GetAll()
        {
            using (var repository = new BaseRepository<ExampleTable, int>())
            {
                var list = repository.Query().Select(x => new Example { Id = x.Id, Title = x.Title}).ToList();
                return list;
            }
        }

    }
}
