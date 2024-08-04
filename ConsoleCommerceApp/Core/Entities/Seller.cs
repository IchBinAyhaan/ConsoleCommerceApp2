using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Seller : Person
    {
        public string Pin { get; set; }
        public string SerialNumber { get; set; }

        public string PhoneNumber {  get; set; }
        public string SellerId { get; set; }
        ICollection<Product> Products { get; set; }
        ICollection<Order> Orders { get; set; }

    }
}
