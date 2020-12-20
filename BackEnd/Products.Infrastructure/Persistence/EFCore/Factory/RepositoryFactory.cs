using System;

using Products.Domain.Repository;

namespace Products.Infrastructure.Persistence.EFCore.Factory
{
    public class RepositoryFactory : Factory
    {
        public override IRepository Create(string repository, DataBaseContext context)
        {
            Type type = Type.GetType(repository);
            return (IRepository)Activator.CreateInstance(type, context);
        }
    }
}
