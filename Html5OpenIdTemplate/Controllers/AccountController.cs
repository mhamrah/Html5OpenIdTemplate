using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Net;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public OpenIdRelyingParty OpenId { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (OpenId == null) {  OpenId = new OpenIdRelyingParty(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            IAuthenticationResponse response = OpenId.GetResponse();

            if (response == null)
            {
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        return OpenId.CreateRequest(id).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException pex)
                    {
                        ModelState.AddModelError("", pex.Message);
                        return View("LogOn");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Identifier");
                    return View("LogOn");
                }

            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        FormsService.SignIn(response.ClaimedIdentifier, true);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("", "Canceled at provider");
                        return View("Login");
                    case AuthenticationStatus.Failed:
                     ModelState.AddModelError("", response.Exception.Message);
                        return View("Login");
                }
            }

            return View("LogOn");
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }

}
