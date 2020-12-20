using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace Products.Domain.Core
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Product> Products { get; set; }

    }
}
