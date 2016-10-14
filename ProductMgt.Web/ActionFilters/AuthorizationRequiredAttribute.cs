using ProductMgt.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (ValidateToken(ctx))
            {
                filterContext.Result = new RedirectResult("~/product/list");
            }
            else if (ValidateSession(ctx))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/auth/login");
            }
        }

        private bool ValidateToken(HttpContext ctx)
        {
            string token = ctx.Request.QueryString["mtoken"];
            if (string.IsNullOrWhiteSpace(token))
                return false;

            IAuthService authProvider = DependencyResolver.Current.GetService<IAuthService>();
            if (authProvider != null && authProvider.ValidateToken(token))
            {
                authProvider.SetSession(ctx);
                return true;
            }

            return false;            
        }

        private bool ValidateSession(HttpContext ctx)
        {
            if (ctx.Session[Constants.LOGIN_KEY] == null)
                return false;

            bool isLoggedIn = (bool)ctx.Session[Constants.LOGIN_KEY];

            return isLoggedIn;
        }
    }
}