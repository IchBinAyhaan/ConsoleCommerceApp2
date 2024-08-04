using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstract
{
    public interface IAdminService
    {
        void AddSeller();
        void AddCustomer();
        void DeleteSeller();
        void DeleteCustomer();
        void GetAllSeller();
        void GetAllCustomer();
        void CreateProductCategory();
        void GetAllOrders();
        void GetOrdersBySeller();
        void GetOrderByCustomer();
        void GetOrderByDate();
    }
}
