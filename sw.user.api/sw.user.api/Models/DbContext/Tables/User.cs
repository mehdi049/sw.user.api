using Microsoft.AspNetCore.Identity;

namespace sw.user.api.Models.DbContext.Tables
{
    public class User
    {
        public User()
        {
            Identity = new ApplicationUser();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Picture { get; set; }
        
        public ApplicationUser Identity { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {

    }

}
