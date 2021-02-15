using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw.user.api.Models
{
    public class Preferences
    {
        public bool DisplayPhoneNumber { get; set; }
        public bool ReceiveNotificationNewArticle { get; set; }
        public bool ReceiveEmail { get; set; }
    }
}
