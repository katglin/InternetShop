using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class Product
    {      
        public long Artikul { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
        public float Cost { get; set; }
        public float Sale { get; set; }
        public float Sale_cost { get; set; }
        public long Amount_store { get; set; }
        public bool Visible { get; set; }
        public string Information { get; set; }
        public long Popularity { get; set; }

<<<<<<< HEAD
        public string ProcName { get; set; }
        public double ProcFreq { get; set; }
        public int HddSize { get; set; }
        public int PhysMem { get; set; }
        public string VideoCard { get; set; }
        public int VideoMem { get; set; }

        //public virtual ICollection<PrCategoryModel> Categories { get; set; }
        //public Product()
        //{
        //    Categories = new List<PrCategoryModel>();
        //}
=======
        public virtual ICollection<PrCategoryModel> Categories { get; set; }
        public Product()
        {
            Categories = new List<PrCategoryModel>();
        }
>>>>>>> origin/Development_Sprint_2
    }
}
