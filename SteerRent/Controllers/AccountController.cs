using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SteerRent.Model;
using SteerRent.Utilities;

namespace SteerRent.Controllers
{
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : Controller
    {
               //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            AccountModel.LogOnModel objLogin = new AccountModel.LogOnModel();
            return View("Login", objLogin);
        }


        public ActionResult Login()
        {
            AccountModel.LogOnModel objLogin = new AccountModel.LogOnModel();
            return View(objLogin);
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult Login(FormCollection frmItem)
        {
            AccountModel.LogOnModel objLoginModel = new AccountModel.LogOnModel();
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(frmItem["txtUserName"], frmItem["txtPassword"]))
                    {
                        FormsAuthentication.SetAuthCookie(frmItem["txtUserName"], false);
                        SessionManager.DisplayName = frmItem["txtUserName"];
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("LoginInvalid", "The user name or password provided is incorrect.");
                    }
            }
            // If we got this far, something failed, redisplay form
            return View(objLoginModel);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            //MembershipUser u = Membership.GetUser("admin");
            //u.ChangePassword(u.ResetPassword(), "admin@123");
            return View();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
