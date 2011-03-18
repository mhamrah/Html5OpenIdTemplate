using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace OpenIdTemplate.App.Auth
{
    public class InMemoryTokenManager : IConsumerTokenManager
    {
        private Dictionary<string, string> tokensAndSecrets = new Dictionary<string, string>();
        public InMemoryTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
          //You don't need to do anything here
        }

        public string GetTokenSecret(string token)
        {
            //You don't need to do anything here
            return string.Empty;
        }

        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        public void StoreNewRequestToken(DotNetOpenAuth.OAuth.Messages.UnauthorizedTokenRequest request, DotNetOpenAuth.OAuth.Messages.ITokenSecretContainingMessage response)
        {
            //You don't need to do anything here
        }
    }
}