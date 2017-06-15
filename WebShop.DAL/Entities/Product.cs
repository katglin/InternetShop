using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Product
    {
        [Key]
        public Int64 Artikul { get; set; }

        public string Name { get; set; }

        public string Mark { get; set; }

        public float Cost { get; set; }

        public float Sale { get; set; }

        public float Sale_cost { get; set; }

        public Int64 Amount_store { get; set; }

        public bool Visible { get; set; }

        public string Information { get; set; }

        public Int64 Popularity { get; set; }

        public Int64 HDD { get; set; }
        public Int64 Memory { get; set; }
        public Int64 Processor { get; set; }
        public Int64 VideoCard { get; set; } 

        public virtual ICollection<PrCategory> Categories { get; set; }
        public Product()
        {
            Categories = new List<PrCategory>();
        }
    }
}
