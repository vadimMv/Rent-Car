using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vadim_final_326960382_91338._16.Security
{
    /// <summary>
    ///custom  authorizetion by role 
    /// </summary>
    public class RolesAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/home/accessdenied");
            }
        }
    }
}