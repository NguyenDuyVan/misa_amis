using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class ErrorService 
    {
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public object Data { get; set; }
    }
}
