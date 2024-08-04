using Application.Services.Concrete;
using Data.Repositories.Concrete;
using Data.UnitOfWork;

public static class Program
{
    private static readonly UnitOfWork _unitOfWork;
    private static readonly AdminService _adminService;
    private static readonly SellerService _sellerService;
    private static readonly CustomerService _customerService;
     static Program()
    {
        _unitOfWork = new UnitOfWork();
        _adminService = new AdminService(_unitOfWork);
        _customerService = new CustomerService(_unitOfWork);
        _sellerService = new SellerService(_unitOfWork);

    }

    public static void Main()
    {
        class Program
    {
        private static IUnitOfWork _unitOfWork;

        static void Main(string[] args)
        {          
            _unitOfWork = new UnitOfWork();
            while (true)
            {
                Console.WriteLine("Select a role (Admin, Seller, Customer) or type 'exit' to quit:");
                string role = Console.ReadLine()?.ToLower();

                if (role == "exit")
                    break;

                switch (role)
                {
                    case "admin":
                        AdminActions();
                        break;

                    case "seller":
                        SellerActions();
                        break;

                    case "customer":
                        CustomerActions();
                        break;

                    default:
                        Console.WriteLine("Invalid role. Please select Admin, Seller, or Customer.");
                        break;
                }
            }
        }

        static void AdminActions()
        {
            var adminService = new AdminService(_unitOfWork);

            while (true)
            {
                Console.WriteLine("Admin Actions:");
                Console.WriteLine("1. Add Seller");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Delete Seller");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. View All Customers");
                Console.WriteLine("6. View All Sellers");
                Console.WriteLine("7. Add Product Category");
                Console.WriteLine("8. View All Orders (Recent First)");
                Console.WriteLine("9. View Orders by Specific Customer");
                Console.WriteLine("10. View Orders by Specific Seller");
                Console.WriteLine("11. View Orders by Date");
                Console.WriteLine("12. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        adminService.AddSeller();
                        break;
                    case "2":
                        adminService.AddCustomer();
                        break;
                    case "3":
                        adminService.DeleteSeller();
                        break;
                    case "4":
                        adminService.DeleteCustomer();
                        break;
                    case "5":
                        adminService.GetAllSeller();
                        break;
                    case "6":
                        adminService.GetAllCustomer();
                        break;
                    case "7":
                        adminService.CreateProductCategory();
                        break;
                    case "8":
                        adminService.GetAllOrders();
                        break;
                    case "9":
                        adminService.GetOrdersBySeller();
                        break;
                    case "10":
                        adminService.GetOrderByCustomer();
                        break;
                    case "11":
                        adminService.GetOrderByDate();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

        static void SellerActions()
        {
            var sellerService = new SellerService(_unitOfWork);

            while (true)
            {
                Console.WriteLine("Seller Actions:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Update Product Quantity");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. View Purchased Products");
                Console.WriteLine("5. View Products by Date");
                Console.WriteLine("6. Filter Products by Name");
                Console.WriteLine("7. View Total Revenue");
                Console.WriteLine("8. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        sellerService.AddProduct();
                        break;
                    case "2":
                        sellerService.UpdateProductQuantity();
                        break;
                    case "3":
                        sellerService.DeleteProduct();
                        break;
                    case "4":
                        sellerService.ViewProductsPurchasedBySeller();
                        break;
                    case "5":
                        sellerService.ViewProductsByDate();
                        break;
                    case "6":
                        sellerService.FilterProductsByName();
                        break;
                    case "7":
                        sellerService.ViewTotalRevenue();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

        static void CustomerActions()
        {
            var customerService = new CustomerService(_unitOfWork);

            while (true)
            {
                Console.WriteLine("Customer Actions:");
                Console.WriteLine("1. Purchase Product");
                Console.WriteLine("2. View Purchased Products");
                Console.WriteLine("3. View Purchased Products by Date");
                Console.WriteLine("4. Filter Products by Name");
                Console.WriteLine("5. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        customerService.BuyProduct();
                        break;
                    case "2":
                        customerService.ViewPurchasedProducts();
                        break;
                    case "3":
                        customerService.ViewPurchasedProductsByDate();
                        break;
                    case "4":
                        customerService.FilterProductsByName();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

    }
}