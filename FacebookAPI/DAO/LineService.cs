using FacebookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FacebookAPI.DAO
{
    public interface ILineService
    {
        Task<dynamic> PushMessageBroadcastAsync(string BearerToken, object message);
    }

    public class LineService : ILineService
    {
        private readonly ILineClient _lineClient;

        public LineService(ILineClient lineClient)
        {
            _lineClient = lineClient;
        }


       

        public async Task<dynamic> PushMessageBroadcastAsync(string BearerToken , object messages)
        {
            var result = await _lineClient.PostAsync<dynamic>(BearerToken, "message/broadcast", new { messages });
            return result;
        }
        
    }
    
}