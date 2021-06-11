using FacebookAPI.DAO;
using FacebookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FacebookAPI.Controllers
{
    public class FacebookController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/GetTokenFacebook")]
        public IHttpActionResult GetTokenFacebook([FromUri] string redirect_uri)
        {
            var FacebookSettings = new Settings();
            var AppId = Settings.FacebookAppId;
            var facebookserviceBase = Settings.facebookserviceBase;
            string redirectFacebook = facebookserviceBase + "dialog/oauth?response_type=token&display=popup&client_id=" + AppId + "&redirect_uri=" + redirect_uri + "&scope=pages_show_list,pages_read_engagement,pages_read_user_content,pages_manage_posts,pages_manage_engagement,public_profile";
            return Redirect(redirectFacebook);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/GetTokenPageFacebook")]
        public async Task<dynamic> GetTokenPageFacebook([FromUri] string access_token, string PageID)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var GetTokenPageFacebookTask = facebookService.GetTokenPageFacebookAsync(access_token, PageID);
            var result = await Task.WhenAll(GetTokenPageFacebookTask);
            var length = result.Length;
            string Returnaccess_token = "";
            dynamic resultExtendAccessToken = null;
            if (length > 0)
            {
                Returnaccess_token = result[0].access_token;
                var ExtendAccessTokenTask = facebookService.ExtendAccessTokenAsync(access_token, PageID);
                resultExtendAccessToken = await Task.WhenAll(ExtendAccessTokenTask);
            }
            return resultExtendAccessToken;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/PostOnPageWall")]
        public async Task<dynamic> PostOnPageWall([FromBody] PostOnPageWall PostOnPageWall)
        {
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var postOnPageWallTask = facebookService.PostOnPageWallAsync(PostOnPageWall.token, PostOnPageWall.PageID, PostOnPageWall.Message);
            var result =  await Task.WhenAll(postOnPageWallTask);
            return result;
        }

      
    }
}
