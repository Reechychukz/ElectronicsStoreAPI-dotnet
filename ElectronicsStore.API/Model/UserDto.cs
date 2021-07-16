using ElectronicsStore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicsStore.API.Model
{
    public class UserDto
    {
        public  string SecurityStamp { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }
        

    }
}
