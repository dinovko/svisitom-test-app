using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UpdateStatusModel
    {
        public bool Status { get; set; }
        public int[] Ids { get; set; }
    }
}
