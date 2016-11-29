using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.Organograma;
using System.IdentityModel;
using System.Security.Permissions;
using Thinktecture.IdentityModel.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [ResourceAuthorize("Autuar","Processo")]
        [HandleForbidden]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Claims()
        {
            ViewBag.Message = "Claims";

            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token");

            if (token != null)
            {
                ViewData["access_token"] = token.Value;
            }

            return View();
        }

        public bool VerificaAutencacao()
        {
            var user = User as ClaimsPrincipal;
            var token = user.FindFirst("access_token");

            if (token != null)            
                return true;            
            else            
                return false;            
        }        

        //exemplo deserializar json data 
        public static T serialized_json_data<T>(string fileJson) where T : new()
        {
            using (var w = new WebClient())
            {
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(fileJson) ? JsonConvert.DeserializeObject<T>(fileJson) : new T();
            }
        }       
    }
}
