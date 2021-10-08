using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMediaAPI.Models
{
    public class LineMessage
    {
        public string BearerToken { get; set; }

        public object to { get; set; }

        public object messages { get; set; }
    }
}