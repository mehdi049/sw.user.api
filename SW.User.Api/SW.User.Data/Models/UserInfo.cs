
namespace SW.User.Data.Models
{
    public class UserInfo
    {
        public Entities.User User { get; set; }
        public int SeenCount { get; set; }
        public int LikesCount { get; set; }
        public int PendingExchangesCount { get; set; }
        public int ExchangesDoneCount { get; set; }
    }
}
