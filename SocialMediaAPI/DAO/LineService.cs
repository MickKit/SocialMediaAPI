using SocialMediaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SocialMediaAPI.DAO
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


        public async Task<dynamic> GenerateJWTToken()
        {
            dynamic arguments = new System.Dynamic.ExpandoObject();
            arguments.privateKey = "{\"alg\":\"RS256\",\"d\":\"WYUYSOcJfcBBcag00bTD3E5sh-4dWneds54DhBDKdwOjW3EJDvrLuDkH1ic95gNOVhW1AtJK2OoohdHaHxH6Yp54zKrcCusY5TtSavADPf8I0PDGfQsZqOnObtr6tRtfvjUpV4AxymuTgMUgKraY9JK8Fy8SYxPrcK-Xp5WvNcGvb9JJRoYha_xIT_ED7AK9cRJHDEFLMcwhCrUc02M5OPYj2vAU6waUw8JNsaSIVSGCXFoeimUxSgFetW1OeYicZ1aUF7Lc46dF1y-VAF8GUAldTQ9Gj443ZjTROw56_RBYWhRN15eGSe0tV8Io86FWy4Sn17vmyjz7SIE66cJM-Q\",\"dp\":\"UorrfEcKejTbx5nqa1XFQNZZpbfnayFp2ZxmWc3fH1TUK6i8d0oVol7EbqBvZPUXbDgsIln3tq8dL8S3dRTN62FEGbxdMOR5d3OvT_NFTN7qLSY0EejX0mp4E-9ZJzEmp6jtxCoSB4oo5HFyQ5pENCD8TDVlOKm0vvPCjopa3RM\",\"dq\":\"gDjaA7tpqBGUWAOynehUkfkvX0-Zq2npSws8n7AP-GrwMhnNlKc026UALkj_xoFvTr5l014zAGa1IebbIR_DMZmvCsTx5qPIcCl6ls6-UN1BqoujNtzRheMs3ZEPGz2ONTpJtZC4ft5ak_TCGwFOoz8jZKyoX6-_VdM-CuEADP0\",\"e\":\"AQAB\",\"ext\":true,\"key_ops\":[\"sign\"],\"kty\":\"RSA\",\"n\":\"yWVd2IlSsTJft4gwqqO7bcEjiFUK-y8YhS_Oql1eAJk2GVSEfIFqpmWAKgh9U4oa0LZ2uqGNu58Zi9PQYLPrIA6VMXbKyZGs2hUI_gbJLCTdEWM8ksWO1GYKCqVQXbJ7D9o-RtyI3jR7CJLfFoMzRSGwl4fvo2Va84EAG2UTlVRvP0P5fAI4FAQJsGPyw7qyUSt3UXifFaX2LBXvcWd3UYUebPCi6QCxEdMmR8H8Uk7g0uHUGvsqcWNXlQKP5SkrDe4vpcvquwfky82RqSeYJYT_jK131jRBq02WWLu-ZibQD62XITVJYdQzynrjxYhcuR1_ctATivjyjbCv9n3VDw\",\"p\":\"5L7NKwNrBLksB7nKFV7dnZi8I5DnmdI4j5BJKE3G1eEktnnIS3_XmyioD-aEqhZeV9RbhSM24P7RlMnQBYB-F2URNjSpqagEuAmDOjDKeNStzn2ayaLQzk4_kZQ3c49ahDY6GUOEe5R9rz_SKB-FLxzEP4oRBHsS2nmq4CbmBlM\",\"q\":\"4WRao_c4dXcStUl3y52ppnXgu9uPVE9FRJeUkxxHxVp7lq7ikR6yQfGl5OnNJN-_StI2PjIupAXHPxvnv2u3JKNls4ymKf4jxgiE_YtK6qZAv7D-WT_sBgwXDHnJfx9JUWQOhz5arMOzzH9YPQ1Pvkl5kX4Ux7QF_5CEJs1k5tU\",\"qi\":\"d-fYmvMUM35ZSqJLDBPys3cVzyoLPZdfpr-f5IQUX6egefCJz1G-kGLMfRYazP_xudXR7xj4FFuFvb0p8a__44ntxmVtfJ6SPRzw5Di369_JuBtFFVjs7EpE6-DSgcCDSXEYu1TvYZPji6529XjU8oc3qlbJZk1mo60mz-96Mv0\"}";
            arguments.kid = "6a01e017-af34-4c89-a645-058f66f7cec0";
            arguments.channelid = "1656074444";

            var result = await _lineClient.PostAsync<dynamic>("", "api/gettoken", arguments);
            return result;
        }

        public async Task<dynamic> GenerateTokenAccessFromJWT(string JWTToken)
        {
        
            var arguments = new List<KeyValuePair<string, string>>();
            arguments.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            arguments.Add(new KeyValuePair<string, string>("client_assertion_type", "urn:ietf:params:oauth:client-assertion-type:jwt-bearer"));
            arguments.Add(new KeyValuePair<string, string>("client_assertion", JWTToken));


            var result = await _lineClient.PosturlencodedAsync<dynamic>("", "token", arguments, "application/x-www-form-urlencoded");
            return result;
        }

    }
    
}