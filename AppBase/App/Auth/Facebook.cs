using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OAuth2;
using AppBase.App.Auth;

namespace AppBase.App.Auth
{
    public class Facebook : IOauthClient
    {
        public  void Authenticate(Uri callback)
        {
            var fb = new FacebookClient();

           fb.RequestUserAuthorization(returnTo:callback);
        }

        public  OpenIdentity Verify()
        {
            var fb = new FacebookClient();

            IAuthorizationState authorization = fb.ProcessUserAuthorization();
        
            var request = WebRequest.Create("https://graph.facebook.com/me?access_token=" + Uri.EscapeDataString(authorization.AccessToken));
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var graph = FacebookGraph.Deserialize(responseStream);
                    return new OpenIdentity()
                               {Id = graph.Id.ToString(), Username = graph.Name, SiteProvider = "Facebook"};
                }
            }
        }
    }
}