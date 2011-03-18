using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SharpAuth
{
    public class TwitterAuth
    {
        public void Init()
        {
            var uri = new UriBuilder("http://api.twitter.com/oauth/request_token");
            
         //   uri

            var req = (HttpWebRequest)WebRequest.Create("");

        //    req.RequestUri.q


        }

    }
}
