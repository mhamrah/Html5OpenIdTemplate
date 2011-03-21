using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using DotNetOpenAuth.OAuth2;

namespace AppBase.App.Auth
{
    //-----------------------------------------------------------------------
    // <copyright file="FacebookGraph.cs" company="Andrew Arnott">
    //     Copyright (c) Andrew Arnott. All rights reserved.
    // </copyright>
    //-----------------------------------------------------------------------

    [DataContract]
    public class FacebookGraph
    {
        private static DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(FacebookGraph));

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        public static FacebookGraph Deserialize(string json)
        {
            if (String.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException("json");
            }

            return Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        }

        public static FacebookGraph Deserialize(Stream jsonStream)
        {
            if (jsonStream == null)
            {
                throw new ArgumentNullException("jsonStream");
            }

            return (FacebookGraph)jsonSerializer.ReadObject(jsonStream);
        }
    }


    public class FacebookClient : WebServerClient
    {

        public FacebookClient()
            : base(new AuthorizationServerDescription
            {
                TokenEndpoint = new Uri("https://graph.facebook.com/oauth/access_token"),
                AuthorizationEndpoint = new Uri("https://graph.facebook.com/oauth/authorize")
            })
        {
            ClientIdentifier = ConfigurationManager.AppSettings["facebookAppID"];
            ClientSecret = ConfigurationManager.AppSettings["facebookAppSecret"];
        }
    }
}
