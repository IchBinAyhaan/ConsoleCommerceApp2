using Core.Constants;
using Core.Entities;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class SellerService
    {
        private readonly UnitOfWork _unitOfWork;
        private IUnitOfWork unitOfWork;

        public SellerService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public SellerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddProduct()
        {
            var product = new Product();

        ProductNameInput: Console.WriteLine("Enter Product Name:");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid input. Product Name cannot be empty.");
                goto ProductNameInput;
            }
            product.Name = name;

        ProductAmountInput:Console.WriteLine("Enter Product Amount:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid input. Amount should be a positive number.");
                goto ProductAmountInput;
            }
            product.Amount = amount;

        ProductQuantityInput:Console.WriteLine("Enter Product Quantity:");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid input. Quantity should be a positive integer.");
                goto ProductQuantityInput;
            }
            product.Quantity = quantity;

        ProductCategoryInput: Console.WriteLine("Enter Product Category ID:");
            if (!int.TryParse(Console.ReadLine(), out int categoryId) || categoryId <= 0)
            {
                Console.WriteLine("Invalid input. Category ID should be a positive integer.");
                goto ProductCategoryInput;
            }
            product.CategoryId = categoryId;

        ProductSellerInput:Console.WriteLine("Enter Product Seller ID:");
            if (!int.TryParse(Console.ReadLine(), out int sellerId) || sellerId <= 0)
            {
                Console.WriteLine("Invalid input. Seller ID should be a positive integer.");
                goto ProductSellerInput;
            }
            product.SellerId = sellerId;

            product.CreatedAt = DateTime.Now;
            product.ModifiedAt = DateTime.Now;

            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();
            Console.WriteLine("Product added successfully.");
        }

        public void UpdateProductQuantity()
        {
        ProductIdInput:Console.WriteLine("Enter Product ID to update quantity:");
            if (!int.TryParse(Console.ReadLine(), out int productId) || productId <= 0)
            {
                Console.WriteLine("Invalid input. Product ID should be a positive integer.");
                goto ProductIdInput;
            }

            var product = _unitOfWork.Products.GetById(productId);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

        ProductQuantityInput: Console.WriteLine("Enter new quantity for the product:");
            if (!int.TryParse(Console.ReadLine(), out int newQuantity) || newQuantity < 0)
            {
                Console.WriteLine("Invalid input. Quantity should be a non-negative integer.");
                goto ProductQuantityInput;
            }
            product.Quantity = newQuantity;
            product.UpdatedAt = DateTime.Now;

            _unitOfWork.Products.Update(product);
            _unitOfWork.Complete();
            Console.WriteLine("Product quantity updated successfully.");
        }
        public void DeleteProduct()
        {
        ProductIdInput:Console.WriteLine("Enter Product ID to delete:");
            if (!int.TryParse(Console.ReadLine(), out int productId) || productId <= 0)
            {
                Console.WriteLine("Invalid input. Product ID should be a positive integer.");
                goto ProductIdInput;
            }

            var product = _unitOfWork.Products.GetById(productId);
            if (product != null)
            {
                _unitOfWork.Products.Remove(product);
                _unitOfWork.Complete();
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        public void ViewProductsPurchasedBySeller()
        {
        SellerIdInput: Console.WriteLine("Enter Seller ID to view purchased products:");
            if (!int.TryParse(Console.ReadLine(), out int sellerId) || sellerId <= 0)
            {
                Console.WriteLine("Invalid input. Seller ID should be a positive integer.");
                goto SellerIdInput;
            }

            var products = _unitOfWork.Products.GetAll().Where(p => p.SellerId == sellerId).ToList();
            if (products.Any())
            {
                Console.WriteLine($"Products purchased from Seller ID {sellerId}:");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products found for the given Seller ID.");
            }
        }
        public void ViewProductsByDate()
        {
        DateInput:Console.WriteLine("Enter date (dd.MM.yyyy) to view products purchased on that date:");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date format. Please enter the date in dd.MM.yyyy format.");
                goto DateInput;
            }

            var products = _unitOfWork.Products.GetAll().Where(p => p.CreatedAt.Date == date.Date).ToList();
            if (products.Any())
            {
                Console.WriteLine($"Products purchased on {date.ToShortDateString()}:");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products found for the given date.");
            }
        }
        public void FilterProductsByName()
        {
        ProductNameInput:Console.WriteLine("Enter part of product name to filter:");
            string nameFilter = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameFilter))
            {
                Console.WriteLine("Invalid input. Product name cannot be empty.");
                goto ProductNameInput;
            }

            var products = _unitOfWork.Products.GetAll().Where(p => p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            if (products.Any())
            {
                Console.WriteLine($"Products containing '{nameFilter}':");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Console.WriteLine("No products found matching the filter criteria.");
            }
        }
        public void ViewTotalRevenue()
        {
            var products = _unitOfWork.Products.GetAll();
            decimal totalRevenue = products.Sum(p => p.Amount * p.Quantity);
            Console.WriteLine($"Total revenue: {totalRevenue:C}");
        }
    }
}
      
