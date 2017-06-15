using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class Memory
    {
        [Key]
        public Int64 Memory_id { get; set; }

        public int Size { get; set; } // in GB
    }
}
