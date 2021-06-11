using FacebookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FacebookAPI.DAO
{
    public interface IFacebookService
    {
        Task<Account> GetAccountAsync(string accessToken);
        Task<dynamic> PostOnPageWallAsync(string accessToken, string PageID, string message);
    }

    public class FacebookService : IFacebookService
    {
        private readonly IFacebookClient _facebookClient;

        public FacebookService(IFacebookClient facebookClient)
        {
            _facebookClient = facebookClient;
        }

        public async Task<Account> GetAccountAsync(string accessToken)
        {
            var result = await _facebookClient.GetAsync<dynamic>("me", "access_token=" + accessToken + "&fields=id,name,email,first_name,last_name,age_range,birthday,gender,locale");

            if (result == null)
            {
                return new Account();
            }

            var account = new Account
            {
                Id = result.id,
                Email = result.email,
                Name = result.name,
                UserName = result.username,
                FirstName = result.first_name,
                LastName = result.last_name,
                Locale = result.locale
            };

            return account;
        }

       

        public async Task<dynamic> PostOnPageWallAsync(string accessToken, string PageID, string message)
        {
            var result = await _facebookClient.PostAsync<dynamic>(accessToken, ""+ PageID + "/feed", new { message });
            return result;
        }

        public async Task<dynamic> ExtendAccessTokenAsync(string accessToken, string PageID)
        {
            var AppId = Settings.FacebookAppId;
            var AppSecret = Settings.FacebookAppSecret;

            var result = await _facebookClient.GetAsync<dynamic>("oauth/access_token", "grant_type=fb_exchange_token&client_id="+ AppId + "&client_secret="+ AppSecret + "&fb_exchange_token="+ accessToken + "");
            return result;
        }

        public async Task<dynamic> GetTokenPageFacebookAsync(string accessToken, string PageID)
        {
            var result = await _facebookClient.GetAsync<dynamic>(PageID, "access_token="+ accessToken + "&fields=access_token");
            return result;
        }

    }
    
}