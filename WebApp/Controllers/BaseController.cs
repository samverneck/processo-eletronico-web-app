using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BaseController: Controller
    {
        public BaseController()
        {
            try
            {
                var user = User as ClaimsPrincipal;
                var token = user.FindFirst("access_token");

                if (token == null)
                {
                    Signout();
                    //Signout(token)
                }
            }
            catch (Exception e)
            {
                Console.Out.Write(e.ToString());
            }
            
        }

        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public void SignoutCleanup(string sid)
        {
            var cp = (ClaimsPrincipal)User;
            var sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }
    }
}