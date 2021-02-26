
using SW.User.Data.Entities;

namespace SW.User.Data.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Picture { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Preference Preference { get; set; }

        public int SeenCount { get; set; }
        public int LikesCount { get; set; }
        public int PendingExchangesCount { get; set; }
        public int ExchangesDoneCount { get; set; }
    }
}
