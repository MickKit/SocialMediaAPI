using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookAPI.Models
{
    public class LineMessage
    {
        public string BearerToken { get; set; }
        public object messages { get; set; }
    }
}