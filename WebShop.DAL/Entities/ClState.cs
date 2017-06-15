using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL
{
    public class ClState
    {
        [Key]
        public Int64 Cl_state_id { get; set; }
        public string Cl_state { get; set; }
    }
}
