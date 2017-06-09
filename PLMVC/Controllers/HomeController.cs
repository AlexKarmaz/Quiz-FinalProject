using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = userService.GetOneByPredicate(u => u.UserName == User.Identity.Name);

                return View();


            }

            return RedirectToAction("Login", "Account");
        }

        //public ActionResult Index()
        //{
        //    //var user = userService.GetById(2);
        //    //ViewBag.IsFriend = user.Password;
        //    //var profile = new BllProfile() { PassedTests = new List<BllTest>(), CreatedTests = new List<BllTest>() };
        //    //var roles = new List<BllRole>();
        //    //var role = roleService.GetOneByPredicate(r => r.Name == "User");
        //    //roles.Add(role);

        //    //var newUser = new BllUser()
        //    //{
        //    //    Id = 1,
        //    //    UserName = "Alex",
        //    //    Password = "11051997",
        //    //    Email = "karmazct@mail.ru",
        //    //    ProfileId = profile.Id,
        //    //    Profile = profile,
        //    //    Roles = roles
        //    //};
        //    //userService.Create(newUser);

        //    return View();
        //}

        [Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}