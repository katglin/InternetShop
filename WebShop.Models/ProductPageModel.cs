using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class ProductPageModel
    {
        public List<string> img = new List<string>();

        public long Artikul { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
        //public List<PrCategoryModel> Categories { get; set; }
        public float Cost { get; set; }
        //[StringLength(2, ErrorMessage = "Поле Скидка не должно превышать 99%", MinimumLength = 1)]
        public float Sale { get; set; }
        public float Sale_cost { get; set; }
        public long Amount_store { get; set; }
        public bool Visible { get; set; }
        public string Information { get; set; }
        public long Popularity { get; set; }

        public Int64 HDD_id { get; set; }
        public int HDD { get; set; }
        public Int64 Memory_id { get; set; }
        public int Memory { get; set; }
        public Int64 Proc_id { get; set; }
        public string ProcessorProducer { get; set; }
        public double ProcessorFreq { get; set; }
        public Int64 Video_id { get; set; }
        public string VideoProducer { get; set; }
        public int VideoMemory { get; set; }
    }
}