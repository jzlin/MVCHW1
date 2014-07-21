using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_HW1.ActionFilters
{
    public class IdFiltersAttribute : ActionFilterAttribute
    {
        public IdFiltersAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 檢查傳入的 id 格式是否符合要求，格式不對就導向回首頁。
            var id = filterContext.RouteData.Values["id"];
            bool isValid = (id == null) ? false : Regex.IsMatch(id.ToString(), @"\d");

            if (isValid)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}