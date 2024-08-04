using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface ISellerService
    {
        void AddProduct();
        void UpdateProductQuantity();
        void DeleteProduct();
        void ViewProductsPurchasedBySeller();
        void ViewProductsByDate();
        void FilterProductsByName();
        void ViewTotalRevenue();
    }
}
