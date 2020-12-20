using System;
using System.Collections.Generic;
using System.Text;

using Products.Domain.Core;
using Products.Domain.Repository;

namespace Products.Infrastructure.Persistence.EFCore.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataBaseContext context) : base(context)
        { }

        public DataBaseContext DBContext
        {
            get { return _context as DataBaseContext; }
        }
    }
}
