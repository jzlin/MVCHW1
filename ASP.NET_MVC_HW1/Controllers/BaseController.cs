using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_HW1.Controllers
{
    public class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            if (this.Request.IsAjaxRequest())
            {
                base.HandleUnknownAction(actionName);
            }
            this.View("NotFoundError").ExecuteResult(this.ControllerContext);
        }
    }
}