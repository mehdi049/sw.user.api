using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.User.Api.Models
{
    public class Response
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
