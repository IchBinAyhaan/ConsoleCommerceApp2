using Data.Contexts;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AdminRepository Admins;
        public readonly SellerRepository Sellers;
        public readonly CustomerRepository Customers;
        public readonly OrderRepository Orders;
        public readonly ProductRepository Products;
        public readonly CategoryRepository Categories;
        public readonly AppDbContext _context;
        public UnitOfWork()
        {
            _context = new AppDbContext();
            Admins = new AdminRepository(_context);
            Sellers = new SellerRepository(_context);
            Customers = new CustomerRepository(_context);
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("Error occurred");
            }

        }
        public void Commit()
        {
            SaveChanges();
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
