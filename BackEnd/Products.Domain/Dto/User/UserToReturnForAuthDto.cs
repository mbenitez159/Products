using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Domain.Dto.User
{
    public class UserToReturnForAuthDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
