using BLL.Interface.Interfaces;
using System;
using PLMVC.Infrastructure.Mappers;
using PLMVC.Models.Profile;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IUserService userService;
        
        public ProfileController(IProfileService profileService, IUserService userService)
        {
            this.userService = userService;
            this.profileService = profileService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name);
                var profile = profileService.GetOneByPredicate(p => p.UserId == user.Id);
                var mvcProfile = profile.ToMvcProfile();
                mvcProfile.UserName = user.UserName;
                mvcProfile.Email = user.Email;
                if (Request.IsAjaxRequest())
                    return PartialView("_Profile", mvcProfile);
                return View("_Profile", mvcProfile);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}