using SocialMediaAPI.DAO;
using SocialMediaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SocialMediaAPI.Controllers
{
    public class LineController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/PushMessageBroadcast")]
        public async Task<dynamic> PushMessageBroadcast([FromBody] LineMessage LineMessage)
        {
            var lineClient = new LineClient("https://api.line.me/v2/bot/");
            var lineService = new LineService(lineClient);
            var PushMessageBroadcast = lineService.PushMessageBroadcastAsync(LineMessage.BearerToken,  LineMessage.messages);
            var result =  await Task.WhenAll(PushMessageBroadcast);
            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/GenerateTokenLine")]
        public async Task<dynamic> GenerateTokenLine()
        {
            dynamic result = "" ;
            var lineClient = new LineClient("http://localhost:8080/");
            var lineService = new LineService(lineClient);
            var GenerateJWTToken = lineService.GenerateJWTToken();
            var resultJWTToken = await Task.WhenAll(GenerateJWTToken);
            string JWTToken = "";
            if (resultJWTToken.Length > 0)
            {
                JWTToken = resultJWTToken[0].JWTToken;

                lineClient = new LineClient("https://api.line.me/oauth2/v2.1/");
                lineService = new LineService(lineClient);
                var GenerateTokenAccessFromJWT = lineService.GenerateTokenAccessFromJWT(JWTToken);
                 result = await Task.WhenAll(GenerateTokenAccessFromJWT);
            }
             
            return result;
        }


    }
}
