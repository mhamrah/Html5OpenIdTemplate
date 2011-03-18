using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpAuth
{
    public class HttpBuilder
    {
        public HttpBuilder()
        {
            QueryVariables = new Dictionary<string, string>();
            HeaderVariables = new Dictionary<string, string>();
        }

        public string Url { get; set;  }
        public Dictionary<string, string> QueryVariables { get; set; }
        public Dictionary<string, string> HeaderVariables { get; set; }
    }
}
