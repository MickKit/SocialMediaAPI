using FacebookAPI.DAO;
using FacebookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FacebookAPI.Controllers
{
    public class LineController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/PushMessageBroadcast")]
        public async Task<dynamic> PushMessageBroadcast([FromBody] LineMessage LineMessage)
        {
            var lineClient = new LineClient();
            var lineService = new LineService(lineClient);
            var PushMessageBroadcast = lineService.PushMessageBroadcastAsync(LineMessage.BearerToken,  LineMessage.messages);
            var result =  await Task.WhenAll(PushMessageBroadcast);
            return result;
        }

      
    }
}
