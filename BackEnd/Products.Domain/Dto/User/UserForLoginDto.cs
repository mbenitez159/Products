using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Products.Domain.Dto.User
{
    public class UserForLoginDto
    {
        [Required (ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email is not Valid")]
        [MaxLength(50,ErrorMessage ="Email max lengh is 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(50, ErrorMessage = "Password max lengh is 50")]
        public string Password { get; set; }
    }
}
