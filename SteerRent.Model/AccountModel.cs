using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteerRent.Model
{
    public class AccountModel
    {
        #region Models

        public class ChangePasswordModel
        {
            [Required]
            //[DataType(DataType.Password)]
            //[Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            //[ValidatePasswordLength]
            //[DataType(DataType.Password)]
            //[Display(Name = "New password")]
            public string NewPassword { get; set; }

            //[DataType(DataType.Password)]
            //[Display(Name = "Confirm new password")]
            //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class LogOnModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            public decimal UserId { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public string StatusMsg { get; set; }
        }


        public class RegisterModel
        {
            [Required]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email address")]
            public string Email { get; set; }

            [Required]
            //[ValidatePasswordLength]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class AspnetMembershipModel
        {
            public Guid ApplicationId {get; set;}
	        public Guid UserId {get; set;}
            public string UserName { get; set; }
	        public string Password {get; set;}
	        public int PasswordFormat {get; set;}
	        public string PasswordSalt {get; set;}
	        public string MobilePIN {get; set;}
	        public string Email {get; set;}
	        public string LoweredEmail {get; set;}
	        public string PasswordQuestion {get; set;}
	        public string PasswordAnswer {get; set;}
	        public bool IsApproved {get; set;}
	        public bool IsLockedOut {get; set;}
	        public DateTime CreateDate {get; set;}
	        public DateTime LastLoginDate {get; set;}
	        public DateTime LastPasswordChangedDate {get; set;}
	        public DateTime LastLockoutDate {get; set;}
	        public int FailedPasswordAttemptCount {get; set;}
	        public DateTime FailedPasswordAttemptWindowStart {get; set;}
	        public int FailedPasswordAnswerAttemptCount {get; set;}
	        public DateTime FailedPasswordAnswerAttemptWindowStart {get; set;}
            public string Comment { get; set; }
        }

        #endregion

        #region Status Codes
        //private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        //{
        //    // See http://go.microsoft.com/fwlink/?LinkID=177550 for
        //    // a full list of status codes.
        //    switch (createStatus)
        //    {
        //        case MembershipCreateStatus.DuplicateUserName:
        //            return "User name already exists. Please enter a different user name.";

        //        case MembershipCreateStatus.DuplicateEmail:
        //            return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

        //        case MembershipCreateStatus.InvalidPassword:
        //            return "The password provided is invalid. Please enter a valid password value.";

        //        case MembershipCreateStatus.InvalidEmail:
        //            return "The e-mail address provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidAnswer:
        //            return "The password retrieval answer provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidQuestion:
        //            return "The password retrieval question provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidUserName:
        //            return "The user name provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.ProviderError:
        //            return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        //        case MembershipCreateStatus.UserRejected:
        //            return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        //        default:
        //            return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        //    }
        //}
        #endregion

        public class SessionTabModel
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
