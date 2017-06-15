using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Processor
    {
        [Key]
        public Int64 Processor_id { get; set; } 

        public string Producer { get; set; }

        public double Frequency { get; set; }
    }
}
