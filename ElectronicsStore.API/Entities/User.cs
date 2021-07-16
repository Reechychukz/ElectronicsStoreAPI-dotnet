using ElectronicsStore.API.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public override string SecurityStamp { get; set; }
        public override string Email { get; set; }
        public override string UserName { get; set; }
        public string Password { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100)]
        public string Address { get; set; }

        public virtual Cart Cart { get; set; }



    }
   
    
}
