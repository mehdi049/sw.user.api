using System;
using System.Collections.Generic;

#nullable disable

namespace sw.user.api.Models.Tables
{
    public partial class SwPreference
    {
        public int Id { get; set; }
        public byte? DisplayPhoneNumber { get; set; }
        public byte? ReceiveNotificationNewArticle { get; set; }
        public byte? ReceiveEmail { get; set; }
        public int FkUserId { get; set; }
    }
}
