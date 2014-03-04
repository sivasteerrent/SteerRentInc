using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SteerRent.Model;

namespace SteerRent.Utilities
{
    public class SessionManager
    {
        #region Session Keys
        private const string _userName = "UserName";
        private const string _customerID = "CustomerID";
        private const string _userRole = "Role";
        private const string _userGuid = "GUID";
        private const string _customerGuid = "CustomerGUID";
        private const string _userID = "UserID";
        private const string _userPassword = "steerrent@123";
        private const string _roleName = "RoleName";
        private const string _isMultipleRole = "IsMultipleRoles";
        private const string _multipleRoleName = "MultipleRoleName";
        private const string _gridFlag = "GridFlag";
        private const string _visitedTabs = "VisitedTabs";
        private const string _checkUserLoggedIn = "CheckLoggedInUser";
        private const string _displayName = "DisplayName";
        private const string _resetPassword = "ResetPassword";
        #endregion

        #region Get & Set Session Values
      
        public string UserPassword
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_userPassword] != null)
                {
                    return HttpContext.Current.Session[_userPassword].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_userPassword] = value;
            }
        }

        public static string checkUserLoggedIn
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_checkUserLoggedIn] != null)
                {
                    return HttpContext.Current.Session[_checkUserLoggedIn].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_checkUserLoggedIn] = value;
            }
        }

        public static string DisplayName
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_displayName ] != null)
                {
                    return HttpContext.Current.Session[_displayName].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_displayName] = value;
            }
        }

        public static string GridFlag
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_gridFlag] != null)
                {
                    return HttpContext.Current.Session[_gridFlag].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_gridFlag] = value;
            }
        }

        public static string roleName
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_roleName] != null)
                {
                    return HttpContext.Current.Session[_roleName].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_roleName] = value;
            }
        }

        public static List<string> multipleRoleNames
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_multipleRoleName] != null)
                {
                    return (List<string>)HttpContext.Current.Session[_multipleRoleName];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_multipleRoleName] = value;
            }
        }

        public static bool isMultipleRole
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_isMultipleRole] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session[_isMultipleRole]);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_isMultipleRole] = value;
            }
        }
       
        public static string userName
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_userName] != null)
                {
                    return HttpContext.Current.Session[_userName].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_userName] = value;
            }
        }

        public static decimal? roleID
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_userRole] != null)
                {
                    return (decimal)(HttpContext.Current.Session[_userRole]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_userRole] = value;
            }
        }

        public static decimal? userGUID
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_userGuid] != null)
                {
                    return (decimal)HttpContext.Current.Session[_userGuid];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_userGuid] = value;
            }
        }

        public static decimal? customerGUID
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_customerGuid] != null)
                {
                    return (decimal)HttpContext.Current.Session[_customerGuid];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_customerGuid] = value;
            }
        }

        public static bool resetPassword
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_resetPassword] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session[_resetPassword]);
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_resetPassword] = value;
            }
        }

        public static decimal? userID
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_userID] != null)
                {
                    return Convert.ToDecimal(HttpContext.Current.Session[_userID]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_userID] = value;
            }
        }

       
        //public static List<AccountModel.SessionTabModel> CustomerVisitedTabs
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session != null && HttpContext.Current.Session[_customervisitedTabs] != null)
        //        {
        //            return (List<AccountModel.SessionTabModel>)HttpContext.Current.Session[_customervisitedTabs];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    set
        //    {
        //        if (HttpContext.Current.Session == null) return;
        //        HttpContext.Current.Session[_customervisitedTabs] = value;
        //    }
        //}

        public static List<AccountModel.SessionTabModel> VisitedTabs
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[_visitedTabs] != null)
                {
                    return (List<AccountModel.SessionTabModel>)HttpContext.Current.Session[_visitedTabs];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (HttpContext.Current.Session == null) return;
                HttpContext.Current.Session[_visitedTabs] = value;
            }
        }
        #endregion


    }
}
