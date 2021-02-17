using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw.user.api.Models.DbContext.Tables
{
    public class Preference
    {
        public Preference()
        {
            User= new User();
        }

        public int Id { get; set; }
        public bool DisplayPhoneNumber { get; set; }
        public bool ReceiveNotificationNewArticle { get; set; }
        public bool ReceiveEmail { get; set; }

        public User User { get; set; }
    }
}
