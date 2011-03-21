using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.RelyingParty;
using System.Net;
using AppBase.Models;

namespace AppBase.Controllers
{
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public OpenIdRelyingParty OpenId { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (OpenId == null) { OpenId = new OpenIdRelyingParty(); }

            base.Initialize(requestContext);
        }

        public ActionResult LogOn(string returnUrl)
        {
            if(!string.IsNullOrEmpty(returnUrl))
                Response.Cookies.Add(new HttpCookie("returnUrl", returnUrl));
    
            if(TempData["auth_error"] != null)
                ModelState.AddModelError("", TempData["auth_error"].ToString());

            return View();
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            return View();
        }

    }

}
