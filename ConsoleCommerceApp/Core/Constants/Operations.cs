using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public enum AdminOperations
    {
        Exit,
         AddSeller,
         AddCustomer,
         DeleteSeller,
         DeleteCustomer,
         GetAllSeller,
         GetAllCustomer,
         CreateProductCategory,
         GetAllOrders,
         GetOrdersBySeller,
         GetOrderByCustomer,
         GetOrderByDate,

    }

    public enum CustomerOperations
    {
        Exit,
         BuyProduc,
         ViewPurchasedProductsByDate,
         ViewPurchasedProducts,
         FilterProductsByName,
    }
    public enum SellerOperations
    {
         Exit,
         AddProduct,
         UpdateProductQuantit,
         DeleteProduct,
         ViewProductsPurchasedBySeller,
         ViewProductsByDate,
         FilterProductsByName,
         ViewTotalRevenue,
    }
}
