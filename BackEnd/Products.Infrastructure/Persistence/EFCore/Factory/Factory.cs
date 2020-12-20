using System;
using System.Collections.Generic;
using System.Text;

using Products.Domain.Repository;

namespace Products.Infrastructure.Persistence.EFCore.Factory
{
    public abstract class Factory
    {
        public abstract IRepository Create(string repository, DataBaseContext context);
    }
}
