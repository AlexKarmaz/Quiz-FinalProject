using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PLMVC.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureName = null;

            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "ru";

            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "ru";
            }
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            // Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }
    }
}