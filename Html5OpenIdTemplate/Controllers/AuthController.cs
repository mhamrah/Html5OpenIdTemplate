using System;
using System.Web.Mvc;
using System.Web.Routing;
using AppBase.App.Auth;
using AppBase.Models;

namespace AppBase.Controllers
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
            return  HandleOpenId(Request.Form["openid_identifier"]);
        }

        public ActionResult Facebook()
        {
            var callback = new Uri(new Uri(string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"))), "/Auth/Callback?type=fb");

            new Facebook().Authenticate(callback);

            return new EmptyResult();
        }

        public ActionResult Google()
        {
             return  HandleOpenId("https://www.google.com/accounts/o8/id");
        
        }

        public ActionResult Yahoo()
        {
          return  HandleOpenId("http://yahoo.com/");
          

        }

        public ActionResult Twitter()
        {
            var callback = new Uri(new Uri(string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"))), "/Auth/Callback?type=tw");

            new Twitter().Authenticate(callback);

            return new EmptyResult();
        }

        public ActionResult Callback(string type)
        {
            IOauthClient client;

            switch (type)
            {
                case "tw":
                    client = new Twitter();
                    break;
                case "fb":
                    client = new Facebook();
                    break;
                default:
                    return ErrorOut("No valid Oauth orchestration was found for current callback.");

            }

            var userInfo = client.Verify();

            return CompleteSignIn(userInfo);
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
        private ActionResult HandleOpenId(string oid)
        {
            try
            {
              var identity =  new OpenId().ProcessOpenId(oid);
              if (identity == null) return new EmptyResult();

                return CompleteSignIn(identity);
            }
            catch (Exception ex)
            {
                return ErrorOut(ex.Message);
            }
        }
        private ActionResult CompleteSignIn(OpenIdentity identity)
        {
            if (identity == null || string.IsNullOrEmpty(identity.Username))
            {
                return ErrorOut("Invalid Identifier");
            }

            _formsService.SignIn(identity.Username, true);

            return Redirect();
        }
    }
}
