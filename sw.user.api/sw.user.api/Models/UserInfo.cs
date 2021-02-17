using System.ComponentModel.DataAnnotations;
using sw.user.api.Models.DbContext.Tables;

namespace sw.user.api.Models
{
    public class UserInfo
    {
        public User User { get; set; }
        public int SeenCount { get; set; }
        public int LikesCount { get; set; }
        public int PendingExchangesCount { get; set; }
        public int ExchangesDoneCount { get; set; }
        public Preferences Preferences { get; set; }
    }
}
