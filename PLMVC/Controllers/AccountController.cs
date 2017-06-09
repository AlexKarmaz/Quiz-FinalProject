﻿using BLL.Interface.Interfaces;
using PLMVC.Models.User;
using PLMVC.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PLMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IProfileService profileService;

        public AccountController(IUserService userService, IProfileService profileService)
        {
            this.userService = userService;
            this.profileService = profileService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (new CustomMembershipProvider().ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (userService.GetOneByPredicate(u => u.Email == viewModel.UserEmail) != null)
            {
                ModelState.AddModelError("", "User with this email already registered.");
                return View(viewModel);
            }

            if (userService.GetOneByPredicate(u => u.UserName == viewModel.UserName) != null)
            {
                ModelState.AddModelError("", "User with this name already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                MembershipRegistration(viewModel);
                FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        [ChildActionOnly]
        public void MembershipRegistration(RegisterViewModel viewModel)
        {
            var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                .CreateUser(viewModel.UserEmail, viewModel.UserName, viewModel.UserPassword);
            //if (membershipUser != null)
            //{
            //    var profile = profileService.GetOneByPredicate(p => p.UserId == viewModel.UserId);
            //    profileService.Update(profile);
            //}
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}