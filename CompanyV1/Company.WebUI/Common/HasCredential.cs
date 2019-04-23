using Company.WebUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.WebUI.Common
{
    public class HasCredential : AuthorizeAttribute
    {
        public string RoleID { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = HttpContext.Current.Session[CommonConstants.USER_SESSION] as UserLogin; 
            if (session == null)
            {
                return false;
            }

            List<string> privilegeLevels = GetCredentialByLoggedInUser(session.UserName); 

            return ((privilegeLevels.Contains(RoleID)) && (session.UserGroupID.Equals(CommonConstants.ADMIN_GROUP) || (session.UserGroupID.Equals(CommonConstants.MEMBER_GROUP)))) ? true : false; 
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }
        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = HttpContext.Current.Session[CommonConstants.CREDENTIAL_SESSION] as List<string>;
            return credentials;
        }
    }
}
