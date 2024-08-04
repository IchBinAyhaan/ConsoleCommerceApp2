using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly List<Product> _products;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _products = new List<Product>();
            _context = context;
        }
        public Product GetById(int productId)
        {
            return _products.SingleOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
    }
}
