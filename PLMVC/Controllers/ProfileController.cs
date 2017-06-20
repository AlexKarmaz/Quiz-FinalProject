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

        //[HttpGet]
        //public ActionResult Edit()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Edit(ProfileEditModel model, HttpPostedFileBase file = null)
        //{
        //    var profileToEdit = profileService.GetOneByPredicate(p => p.UserName == User.Identity.Name);

        //    model.Id = profileToEdit.Id;

        //    ImageSetUp(model.Id, file);

        //    profileService.Update(model.ToUpdatingBllProfile());

        //    return RedirectToAction("Index");
        //}

       
        //public ActionResult ShowUser(int id)
        //{

        //    var profile = profileService.GetById(id).ToFullMvcProfile();

        //    var curUserProfileId = profileService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;

        //    if (friendshipService.IsFriend(curUserProfileId, id))
        //        ViewBag.IsFriend = true;
        //    else if (friendshipService.IsRequested(curUserProfileId, id))
        //        ViewBag.IsFriend = null;
        //    else ViewBag.IsFriend = false;

        //    return View("_Profile", profile);
        //}
    }
}