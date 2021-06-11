using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacebookAPI.Models
{
    public class PostOnPageWall
    {
        public string token { get; set; }
        public string PageID { get; set; }
        public string Message { get; set; }

    }
}