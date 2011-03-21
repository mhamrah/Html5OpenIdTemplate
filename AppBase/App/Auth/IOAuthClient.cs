using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppBase.App.Auth
{
    interface IOauthClient
    {
        void Authenticate(Uri callback);
        OpenIdentity Verify();
    }
}
