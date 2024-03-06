using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entidades.Entidades
{
    public class ApplicationUser:IdentityUser
    {
        public int Idade { get; set; }
        public string Celular { get; set; } 
    }
}
