using Application.Services.Abstract;
using Core.Constants;
using Core.Entities;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Concrete
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void BuyProduct()
        {
            var products = _unitOfWork.Products.GetAll().ToList();
            if (!products.Any())
            {
                Messages.WarningMessage("products");
                return;
            }

            Console.WriteLine("Available products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Amount: {product.Amount}, Quantity: {product.Quantity}");
            }

        ProductSelectionInput:Messages.InputMessage("Product ID to purchase");
            if (!int.TryParse(Console.ReadLine(), out int productId) || productId <= 0)
            {
                Messages.InvalidInputMessage("Product ID (positive integer)");
                goto ProductSelectionInput;
            }

            var selectedProduct = _unitOfWork.Products.GetById(productId);
            if (selectedProduct == null)
            {
                Messages.NotFoundMessage("Product");
                return;
            }

        QuantityInput:Messages.InputMessage("Quantity to purchase");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0 || quantity > selectedProduct.Quantity)
            {
                Messages.InvalidInputMessage("Quantity (positive integer and less than or equal to available quantity)");
                goto QuantityInput;
            }

            decimal totalPrice = selectedProduct.Amount * quantity;
            Console.WriteLine($"Total price: {totalPrice:C}");

        ConfirmPurchase:Messages.InputMessage("confirm purchase (y or n)");
            var confirmation = Console.ReadLine()?.ToLower();
            if (confirmation != "y" && confirmation != "n")
            {
                Messages.InvalidInputMessage("confirmation input (y or n)");
                goto ConfirmPurchase;
            }

            if (confirmation == "y")
            {
                selectedProduct.Quantity -= quantity;
                _unitOfWork.Products.Update(selectedProduct);
                var order = new Order
                {
                    ProductId = selectedProduct.ProductId,
                    Quantity = quantity,
                    TotalPrice = totalPrice,
                    PurchasedAt = DateTime.Now
                };
                _unitOfWork.Orders.Add(order);
                _unitOfWork.Complete();
                Messages.SuccessMessage("Purchase", "completed");
            }
            else
            {
                Console.WriteLine("Purchase cancelled.");
            }
        }
        public void ViewPurchasedProducts()
        {
            Console.WriteLine("Enter Customer ID to view purchased products:");
            if (!int.TryParse(Console.ReadLine(), out int customerId) || customerId <= 0)
            {
                Messages.InvalidInputMessage("Customer ID (positive integer)");
                return;
            }

            var orders = _unitOfWork.Orders.GetAll().Where(o => o.CustomerId == customerId).ToList();
            if (orders.Any())
            {
                Console.WriteLine($"Products purchased by Customer ID {customerId}:");
                foreach (var order in orders)
                {
                    var product = _unitOfWork.Products.GetById(order.ProductId);
                    Console.WriteLine($"Order ID: {order.OrderId}, Product Name: {product?.Name}, Quantity: {order.Quantity}, Total Price: {order.TotalPrice:C}");
                }
            }
            else
            {
                Messages.WarningMessage("purchased products");
            }
        }
        public void ViewPurchasedProductsByDate()
        {
        DateInput:Messages.InputMessage("date (dd.MM.yyyy) to view products purchased on that date");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Messages.InvalidInputMessage("date in dd.MM.yyyy format");
                goto DateInput;
            }

            var orders = _unitOfWork.Orders.GetAll().Where(o => o.PurchasedAt.Date == date.Date).ToList();
            if (orders.Any())
            {
                Console.WriteLine($"Products purchased on {date.ToShortDateString()}:");
                foreach (var order in orders)
                {
                    var product = _unitOfWork.Products.GetById(order.ProductId);
                    Console.WriteLine($"Order ID: {order.OrderId}, Product Name: {product?.Name}, Quantity: {order.Quantity}, Total Price: {order.TotalPrice:C}");
                }
            }
            else
            {
                Messages.WarningMessage("purchased products on the specified date");
            }
        }
        public void FilterProductsByName()
        {
        ProductNameInput:Messages.InputMessage("part of product name to filter");
            string nameFilter = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nameFilter))
            {
                Messages.InvalidInputMessage("Product name");
                goto ProductNameInput;
            }

            var products = _unitOfWork.Products.GetAll().Where(p => p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            if (products.Any())
            {
                Console.WriteLine($"Products containing '{nameFilter}':");
                foreach (var product in products)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Amount: {product.Amount}, Quantity: {product.Quantity}");
                }
            }
            else
            {
                Messages.WarningMessage("products matching the filter criteria");
            }
        }
    }
}
