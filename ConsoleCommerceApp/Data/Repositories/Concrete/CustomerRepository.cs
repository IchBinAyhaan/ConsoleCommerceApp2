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
    public class CustomerRepository : Repository<Customer>,ICustomerRepository
    {
        private readonly AppDbContext _context;
        private object _customers;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Customer GetById(int id)
        {
            return _customers.Find(id);
        }

        public bool Remove(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
