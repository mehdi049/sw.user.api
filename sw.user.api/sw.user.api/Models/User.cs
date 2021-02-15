using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sw.user.api.Models.Tables;

namespace sw.user.api.Models
{
    public class User
    {
        public SwUser SwUser { get; set; }

        public int SeenCount { get; set; }
        public int LikesCount { get; set; }
        public int PendingExchangesCount { get; set; }
        public int ExchangesDoneCount { get; set; }

        public Preferences Preferences { get; set; }
    }
}
