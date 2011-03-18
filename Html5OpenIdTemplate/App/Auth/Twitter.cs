using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using OpenIdTemplate.App.Auth;
using OpenIdTemplate.Controllers;

namespace AppBase.App.Auth
{
    public class Twitter
    {
        private ServiceProviderDescription GetServiceProviderDescription()
        {
            return new ServiceProviderDescription
            {
                RequestTokenEndpoint = new MessageReceivingEndpoint("http://api.twitter.com/oauth/request_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint("http://api.twitter.com/oauth/authenticate", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                AccessTokenEndpoint = new MessageReceivingEndpoint("http://api.twitter.com/oauth/access_token", HttpDeliveryMethods.GetRequest | HttpDeliveryMethods.AuthorizationHeaderRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
            };
        }

        private IConsumerTokenManager GetTokenManager()
        {
            var store = System.Web.HttpContext.Current.Session;
                var tokenManager = (InMemoryTokenManager)store["TwitterShortTermUserSessionTokenManager"];
                if (tokenManager == null)
                {
                    string consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                    string consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];

                    tokenManager = new InMemoryTokenManager(consumerKey, consumerSecret);
                    store["TwitterShortTermUserSessionTokenManager"] = tokenManager;
                }


                return tokenManager;
        }
        private WebConsumer GetConsumer()
        {
            return new WebConsumer(GetServiceProviderDescription(), GetTokenManager());
        }
        public void Authenticate(Uri callback)
        {
            var redirectParameters = new Dictionary<string, string>();
            //   redirectParameters["force_login"] = "true";

            var twitter = GetConsumer();

            var request = twitter.PrepareRequestUserAuthorization(callback, null, redirectParameters);

            twitter.Channel.PrepareResponse(request).Send();

        }

        public string Verify()
        {
            var twitter = GetConsumer();

            var response = twitter.ProcessUserAuthorization();

            if (response == null)
                return null;

            var screenName = response.ExtraData["screen_name"];
            var userId = int.Parse(response.ExtraData["user_id"]);

            return screenName;
        }
    }
}