using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer : Person
    {
        public string PhoneNumber { get; set; }
        public string Pin { get; set; }
        public string SerialNumber { get; set; }
        public string CustomerId { get; set; }
    }
}
