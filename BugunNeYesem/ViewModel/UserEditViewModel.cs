using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugunNeYesem.ViewModel
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
    }
}
