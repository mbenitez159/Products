using System;
using System.Threading.Tasks;

using Products.Domain.Repository;

namespace Products.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get;}
        void Initialize();
        Task<bool> Complete();
    }
}
