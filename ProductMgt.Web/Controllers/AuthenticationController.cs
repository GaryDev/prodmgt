﻿using ProductMgt.Service;
using ProductMgt.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.Controllers
{
    [RoutePrefix("auth")]
    public class AuthenticationController : Controller
    {
        private IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        public ViewResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authService.Authenticate(model.UserName, model.Password))
                {
                    _authService.SetSession(HttpContext.ApplicationInstance.Context);
                    return Redirect(returnUrl ?? Url.Action("List", "Product"));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "用户名或密码错误");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}