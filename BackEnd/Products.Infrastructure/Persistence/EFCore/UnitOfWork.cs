using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

using Products.Domain.Repository;
using Products.Domain.UnitOfWork;
using Products.Infrastructure.Helper;
using Products.Infrastructure.Persistence.EFCore.Factory;

namespace Products.Infrastructure.Persistence.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;

        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }

        public IProductRepository Products { get; private set; }

        public void Initialize()
        {
            var repositories = this.GetAttributeBy<IRepository>();
            foreach (var repository in repositories)
            {
                var repositoryInstance = new RepositoryFactory().Create(repository.Name, _context);
                this.SetProperty(repository, repositoryInstance);
            }
        }



        public async Task<bool> Complete()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync();
                transaction.Commit();
                return result > 0;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                ExceptionDispatchInfo
                     .Capture(ex.InnerException)
                     .Throw();
            }
            return false;
        }

        public void Dispose()
            => _context.Dispose();


    }
}
