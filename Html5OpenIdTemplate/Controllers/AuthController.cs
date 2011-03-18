using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AppBase.App.Auth;
using AppBase.Models;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace OpenIdTemplate.Controllers
{
    public class AuthController : Controller
    {

        public IFormsAuthenticationService _formsService { get; set; }
      
        protected override void Initialize(RequestContext requestContext)
        {
            if (_formsService == null) { _formsService = new FormsAuthenticationService(); }
        
            base.Initialize(requestContext);
        }

        [ValidateInput(false)]
        public ActionResult OpenId()
        {
            return new OpenId().ProcessOpenId(Request.Form["openid_identifier"]);
        }

        public ActionResult Facebook()
        {
            return new EmptyResult();
        }

        public ActionResult Google()
        {
            return new OpenId().ProcessOpenId("https://www.google.com/accounts/o8/id");
        }

        public ActionResult Yahoo()
        {
            return new OpenId().ProcessOpenId("http://yahoo.com/");
        }

        public ActionResult Twitter()
        {
            Uri callback = new Uri(new Uri(string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"))), "/Auth/Callback");

            new AppBase.App.Auth.Twitter().Authenticate(callback);

            return new EmptyResult();
        }

        public ActionResult Callback()
        {
            var username = new AppBase.App.Auth.Twitter().Verify();

            if (string.IsNullOrEmpty(username))
            {
                return ErrorOut("Invalid Identifier");
            }

            _formsService.SignIn(username, true);

            return Redirect();
        }

        private ActionResult ErrorOut(string message)
        {
            ModelState.AddModelError("", message);
            return RedirectToAction("LogOn", "Account");
        }

        private ActionResult Redirect()
        {
            var cookie = Request.Cookies["returnUrl"];

            if (cookie != null)
            {
                return Redirect(cookie.Value);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
