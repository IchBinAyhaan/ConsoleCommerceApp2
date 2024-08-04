using Core.Entities;
using Data.Contexts;
using Data.Repositories.Abstract;
using Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class SellerRepository : Repository<Seller>,ISellerRepository
    {
        private readonly AppDbContext _context;
        private object _sellers;

        public SellerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Seller GetById(int sellerId)
        {
        
        }

        public bool Remove(Seller seller)
        {
            throw new NotImplementedException();
        }
    }
}
