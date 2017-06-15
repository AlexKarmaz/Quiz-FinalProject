using BLL.Interface.Interfaces;
using PLMVC.Models.User;
using PLMVC.Providers;
using PLMVC.Infrastructure.Mappers;
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
                    return RedirectToAction("ShowLastTests", "Test");
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
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                .CreateUser(viewModel.UserEmail, viewModel.UserName, viewModel.UserPassword);
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
                    return RedirectToAction("ShowLastTests", "Test");
                }
            }

            return View(viewModel);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            string[] roles;
            int i = 0;
            var users = userService.GetAllByPredicate(u => u.UserName != User.Identity.Name);
            var mvcUsers = users.Select(u => u.ToMvcAllUsers());
            ShowUsersViewModel[] newUsers = new ShowUsersViewModel[mvcUsers.Count()];
            foreach (var mvcUser in mvcUsers)
            {
                roles = userService.GetRolesForUser(mvcUser.UserName);
                // mvcUser.RoleNames = userService.GetRolesForUser(mvcUser.UserName).ToList();
              //  mvcUser = new ShowUsersViewModel { Id = mvcUser.Id, Email = mvcUser.Email, UserName = mvcUser.UserName, RoleNames = string.Join(",", roles) };
                mvcUser.RoleNames = string.Join(",", roles);
                newUsers[i] = new ShowUsersViewModel { Id = mvcUser.Id, Email = mvcUser.Email, UserName = mvcUser.UserName, RoleNames = string.Join(",", roles) };
                i++;
            }
          //  ViewBag.Roles = mvcUsers.Select(u => u.RoleNames = userService.GetRolesForUser(u.UserName));
            if (Request.IsAjaxRequest())
                return PartialView("_ShowAllUsers",newUsers);
            return View("_ShowAllUsers",mvcUsers);
        }
    }
}