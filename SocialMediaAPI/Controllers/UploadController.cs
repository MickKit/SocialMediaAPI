using SocialMediaAPI.DAO;
using SocialMediaAPI.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SocialMediaAPI.Controllers
{
    public class UploadController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/uploadpicture")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var files = httpRequest.Files;
                var pathfile = "picture";
              

                var folderName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, pathfile);
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                if (files.Count == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                var list = new List<dynamic>();
                var fileName = "";
                var dbPath = "";
                foreach (string file in files)
                {
                    var postedFile = httpRequest.Files[file];
                    fileName = postedFile.FileName.Trim('"');
                    var fullPath = Path.Combine(folderName, fileName);
                    dbPath = Path.Combine(pathfile, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                    postedFile.SaveAs(fullPath);

                    dynamic obj = new ExpandoObject();
                    obj.fileName = fileName;
                    obj.dbPath = dbPath;
                    list.Add(obj);

                }
           
                return Request.CreateResponse(HttpStatusCode.Created, list);

            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);

            }
        }

      

    }
}
