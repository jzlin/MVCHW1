using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_HW1.Models;

namespace ASP.NET_MVC_HW1.Controllers
{
    public class ReportController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        private 客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();
        private SimpleReportRepository simpleReportRepo = RepositoryHelper.GetSimpleReportRepository();

        // GET: Report
        public ActionResult Index()
        {
            //return View(db.SimpleReport.ToList());

            return View(simpleReportRepo.All());
        }

        // GET: Report/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SimpleReport simpleReport = db.SimpleReport.Find(id);

            SimpleReport simpleReport = simpleReportRepo.FindById(id.Value);

            if (simpleReport == null)
            {
                return HttpNotFound();
            }
            return View(simpleReport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();

                simpleReportRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
