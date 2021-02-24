using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SW.User.Data.Common
{
    public class Response
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
