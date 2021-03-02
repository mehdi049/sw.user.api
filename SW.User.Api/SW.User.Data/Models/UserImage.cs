using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.User.Data.Models
{
    public class UserImage
    {
        public string UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
