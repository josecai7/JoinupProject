using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageArray { get; set; }
    }
}
