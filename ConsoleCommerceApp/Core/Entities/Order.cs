using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int SellerId { get; set; }   
        public Seller Seller { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public object OrderDate { get; set; }
        public object OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}
