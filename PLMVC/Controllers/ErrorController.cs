using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logger.Interfaces;

namespace PLMVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger logger)
        {
            this.logger = logger;
        }
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            logger.Error("Not Found error 404");
            return View();
        }

        public ActionResult Error()
        {
            Response.StatusCode = 400;
            logger.Error("Bad request error 400");
            return View();
        }

        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            logger.Error("Server error 500");
            return View();
        }
    }
}