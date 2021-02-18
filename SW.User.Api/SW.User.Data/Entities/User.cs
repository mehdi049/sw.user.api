using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SW.User.Data.Entities
{
    public class User
    {
        public User()
        {
            Preference = new Preference();
        }

        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(20)]
        [Required]
        public string Gender { get; set; }

        [MaxLength(50)]
        [Required]
        public string City { get; set; }

        [MaxLength(50)]
        [Required]
        public string Region { get; set; }

        [MaxLength(100)]
        public string Picture { get; set; }

        public int PreferenceId { get; set; }
        public Preference Preference { get; set; }

        public string IdentityId { get; set; }
        public IdentityUser Identity { get; set; }
    }

}
