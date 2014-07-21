using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC_HW1.Models;
using ASP.NET_MVC_HW1.ActionFilters;

namespace ASP.NET_MVC_HW1.Controllers
{
    public class ContactController : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        private 客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();
        private 客戶聯絡人Repository contactRepo = RepositoryHelper.Get客戶聯絡人Repository();

        // GET: Contact
        public ActionResult Index()
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            //return View(客戶聯絡人.ToList());

            return View(contactRepo.All().Include(c => c.客戶資料));
        }

        // GET: Contact/Details/5
        [IdFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);

            客戶聯絡人 客戶聯絡人 = contactRepo.FindById(id.Value);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();

                contactRepo.Add(客戶聯絡人);
                contactRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        [IdFilters]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);

            客戶聯絡人 客戶聯絡人 = contactRepo.FindById(id.Value);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IdFilters]
        public ActionResult Edit(int Id, FormCollection form)
        {
            客戶聯絡人 客戶聯絡人 = contactRepo.FindById(Id);
            if (TryUpdateModel<I客戶聯絡人更新>(客戶聯絡人))
            {
                contactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }
        //public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.Entry(客戶聯絡人).State = EntityState.Modified;
        //        //db.SaveChanges();

        //        contactRepo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
        //        contactRepo.UnitOfWork.Commit();

        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);

        //    ViewBag.客戶Id = new SelectList(clientRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
        //    return View(客戶聯絡人);
        //}

        // GET: Contact/Delete/5
        [IdFilters]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);

            客戶聯絡人 客戶聯絡人 = contactRepo.FindById(id.Value);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [IdFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            //db.SaveChanges();

            contactRepo.Remove(id);
            contactRepo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();

                contactRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
