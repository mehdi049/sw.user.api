using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw.user.api.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Email { get; set; }
    }
}
