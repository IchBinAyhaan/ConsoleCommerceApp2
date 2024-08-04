using Application.Services.Abstract;
using Core.Constants;
using Core.Entities;
using Data.Contexts;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Application.Services.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly UnitOfWork _unitOfWork;
        private List<Seller> _sellers;
        private List<Customer> _customers;
        private IUnitOfWork unitOfWork;

        public AdminService(UnitOfWork unitOfWork)
        {
            _sellers = new List<Seller>();
            _customers = new List<Customer>();
            _unitOfWork = unitOfWork;
          
        }

        public AdminService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddSeller()
        {
            Seller seller = new Seller();
            Console.WriteLine("Create a new seller:");

        SellerNameInput:Messages.InputMessage("Seller First Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMessage("Seller First Name");
                goto SellerNameInput;
            }
            seller.Name = name;

        SellerLastNameInput:Messages.InputMessage("Seller Last Name");
            string surname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname))
            {
                Messages.InvalidInputMessage("Seller Last Name");
                goto SellerLastNameInput;
            }
            seller.Surname = surname;

        SellerEmailInput: Messages.InputMessage("Seller Email");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                Messages.InvalidInputMessage("Seller Email");
                goto SellerEmailInput;
            }
            seller.Email = email;

        SellerPhoneInput:Messages.InputMessage("Seller Phone Number");
            string phoneNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                Messages.InvalidInputMessage("Seller Phone Number");
                goto SellerPhoneInput;
            }
            seller.PhoneNumber = phoneNumber;

        SellerPinInput:Messages.InputMessage("Seller Pin");
            string pin = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(pin))
            {
                Messages.InvalidInputMessage("Seller Pin");
                goto SellerPinInput;
            }
            seller.Pin = pin;

        InputSellerSerialNumber:Messages.InputMessage("Serial Number");
            string sellerSerialNumber = Console.ReadLine();
            if (string.IsNullOrEmpty(sellerSerialNumber) || sellerSerialNumber.Length != 9)
            {
                Messages.InvalidInputMessage("Serial Number");
                goto InputSellerSerialNumber;
            }
        }

        public void DeleteSeller()
        {
            Console.WriteLine("Delete a seller:");

        SellerIdInput:Messages.InputMessage("Seller ID");
            if (!int.TryParse(Console.ReadLine(), out int sellerId) || sellerId <= 0)
            {
                Messages.InvalidInputMessage("Seller ID");
                goto SellerIdInput;
            }

            var seller = _unitOfWork.Sellers.GetById(sellerId);
            if (seller != null)
            {
                bool isSuccess = _unitOfWork.Sellers.Remove(seller);
                if (isSuccess)
                {
                    _unitOfWork.Complete();
                    Messages.SuccessMessage("Seller", "deleted");
                }
                else
                {
                    Messages.ErrorOccuredMessage();
                }
            }
            else
            {
                Messages.NotFoundMessage("Seller");
            }
        }

        public void GetAllSeller()
        {
            var sellers = _unitOfWork.Sellers.GetAll();
            if (sellers.Any())
            {
                Console.WriteLine("Sellers:");
                foreach (var seller in sellers)
                {
                    Console.WriteLine($"{seller.SellerId}: {seller.Name} {seller.Surname} ({seller.Email})");
                }
            }
            else
            {
                Messages.WarningMessage("sellers");
            }
        }
        public void AddCustomer()
        {
            Customer customer = new Customer();
            Console.WriteLine("Create a new customer:");

        CustomerNameInput: Messages.InputMessage("Customer First Name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInputMessage("Customer First Name");
                goto CustomerNameInput;
            }
            customer.Name = name;

        CustomerLastNameInput:Messages.InputMessage("Customer Last Name");
            string surname = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(surname))
            {
                Messages.InvalidInputMessage("Customer Last Name");
                goto CustomerLastNameInput;
            }
            customer.Surname = surname;

        CustomerEmailInput:Messages.InputMessage("Customer Email");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                Messages.InvalidInputMessage("Customer Email");
                goto CustomerEmailInput;
            }
            customer.Email = email;
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.SaveChanges(); 
            Messages.SuccessMessage("Customer", "created");
        }

        public void DeleteCustomer()
        {
            Console.WriteLine("Delete a customer:");

        CustomerIdInput: Messages.InputMessage("Customer ID");
            if (!int.TryParse(Console.ReadLine(), out int customerId) || customerId <= 0)
            {
                Messages.InvalidInputMessage("Customer ID");
                goto CustomerIdInput;
            }

            var customer = _unitOfWork.Customers.GetById(customerId);
            if (customer != null)
            {
                bool isSuccess = _unitOfWork.Customers.Remove(customer);
                if (isSuccess)
                {
                    _unitOfWork.Complete();
                    Messages.SuccessMessage("Customer", "deleted");
                }
                else
                {
                    Messages.ErrorOccuredMessage();
                }
            }
            else
            {
                Messages.NotFoundMessage("Customer");
            }
        }

        public void GetAllCustomer()
        {
            var customers = _unitOfWork.Customers.GetAll();
            if (customers.Any())
            {
                Console.WriteLine("Customers:");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.CustomerId}: {customer.Name} {customer.Surname} ({customer.Email})");
                }
            }
            else
            {
                Messages.WarningMessage("customers");
            }
        }
        public void GetAllOrders()
        {
            var orders = _unitOfWork.Orders.GetAll().OrderByDescending(o => o.OrderDate).ToList();
            if (orders.Any())
            {
                Console.WriteLine("Orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Customer ID: {order.CustomerId}, Seller ID: {order.SellerId}");
                }
            }
            else
            {
                Messages.WarningMessage("orders");
            }
        }

        public void GetOrderByCustomer()
        {
            Console.WriteLine("List orders for a specific customer:");

        CustomerIdInput: Messages.InputMessage("Customer ID");
            if (!int.TryParse(Console.ReadLine(), out int customerId) || customerId <= 0)
            {
                Messages.InvalidInputMessage("Customer ID");
                goto CustomerIdInput;
            }

            var orders = _unitOfWork.Orders.GetAll().Where(o => o.CustomerId == customerId).ToList();
            if (orders.Any())
            {
                Console.WriteLine($"Orders for Customer ID {customerId}:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Seller ID: {order.SellerId}");
                }
            }
            else
            {
                Messages.WarningMessage("orders for the customer");
            }
        }

        public void GetOrdersBySeller()
        {
            Console.WriteLine("List orders for a specific seller:");

        SellerIdInput:Messages.InputMessage("Seller ID");
            if (!int.TryParse(Console.ReadLine(), out int sellerId) || sellerId <= 0)
            {
                Messages.InvalidInputMessage("Seller ID");
                goto SellerIdInput;
            }

            var orders = _unitOfWork.Orders.GetAll().Where(o => o.SellerId == sellerId).ToList();
            if (orders.Any())
            {
                Console.WriteLine($"Orders for Seller ID {sellerId}:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Customer ID: {order.CustomerId}");
                }
            }
            else
            {
                Messages.WarningMessage("orders for the seller");
            }
        }

        public void GetOrderByDate()
        {
            Console.WriteLine("List orders for a specific date:");

        DateInput: Messages.InputMessage("Date (dd.MM.yyyy)");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Messages.InvalidInputMessage("Date");
                goto DateInput;
            }
            var orders = _unitOfWork.Orders.GetAll().Where(o => o.OrderDate.Date == date.Date).ToList();
            if (orders.Any())
            {
                Console.WriteLine($"Orders on {date.ToShortDateString()}:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Customer ID: {order.CustomerId}, Seller ID: {order.SellerId}");
                }
            }
            else
            {
                Messages.WarningMessage("orders for the date");
            }
        }
        public void CreateProductCategory()
        {
            Console.WriteLine("Add a new category:");

        CategoryNameInput:Messages.InputMessage("Category Name");
            string categoryName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                Messages.InvalidInputMessage("Category Name");
                goto CategoryNameInput;
            }

            var category = new Category
            {
                Name = categoryName
            };

            bool isSuccess = _unitOfWork.Categories.Add(category);
            if (isSuccess)
            {
                _unitOfWork.Complete();
                Messages.SuccessMessage("Category", "added");
            }
            else
            {
                Messages.ErrorOccuredMessage();
            }

        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}





