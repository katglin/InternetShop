using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class OrderState
    {
        [Key]
        public Int64 State_id { get; set; }

        public string State { get; set; }
    }
}
