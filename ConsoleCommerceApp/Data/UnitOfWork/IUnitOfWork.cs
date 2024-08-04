using Data.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        void Complete();
        void Commit();
    }
}
