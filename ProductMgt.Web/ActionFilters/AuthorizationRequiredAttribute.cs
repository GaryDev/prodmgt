using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string LOGIN_KEY = "LoggedIn";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isLoggedIn = (bool)filterContext.HttpContext.Session[LOGIN_KEY];
            if (!isLoggedIn)
            {
                filterContext.Result = new RedirectResult("auth/login");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}