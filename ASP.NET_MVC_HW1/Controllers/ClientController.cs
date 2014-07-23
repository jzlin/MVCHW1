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
using AutoMapper;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ASP.NET_MVC_HW1.Controllers
{
    public class ClientController : BaseController
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        private 客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();
        private 客戶銀行資訊Repository bankRepo = RepositoryHelper.Get客戶銀行資訊Repository();
        private 客戶聯絡人Repository contactRepo = RepositoryHelper.Get客戶聯絡人Repository();

        // GET: Client
        public ActionResult Index()
        {
            //return View(db.客戶資料.ToList());

            return View(clientRepo.All());
        }

        // GET: Client/Details/5
        [IdFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);

            客戶資料 客戶資料 = clientRepo.FindById(id.Value);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewBag.Contacts = clientRepo.get客戶聯絡人ById(id.Value).ToList();
            ViewBag.Banks = clientRepo.get客戶銀行資訊ById(id.Value).ToList();

            return View(客戶資料);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();

                clientRepo.Add(客戶資料);
                clientRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Client/Edit/5
        [IdFilters]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);

            客戶資料 客戶資料 = clientRepo.FindById(id.Value);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }

            ViewBag.Contacts = clientRepo.get客戶聯絡人ById(id.Value).ToList();
            ViewBag.Banks = clientRepo.get客戶銀行資訊ById(id.Value).ToList();

            return View(客戶資料);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [IdFilters]
        public ActionResult Edit(int Id, FormCollection form)
        {
            客戶資料 客戶資料 = clientRepo.FindById(Id);
            if (TryUpdateModel<I客戶資料更新>(客戶資料))
            {
                clientRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }
        //public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.Entry(客戶資料).State = EntityState.Modified;
        //        //db.SaveChanges();

        //        clientRepo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
        //        clientRepo.UnitOfWork.Commit();

        //        return RedirectToAction("Index");
        //    }
        //    return View(客戶資料);
        //}

        // GET: Client/Delete/5
        [IdFilters]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);

            客戶資料 客戶資料 = clientRepo.FindById(id.Value);

            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [IdFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            //db.SaveChanges();

            clientRepo.Remove(id);
            clientRepo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult ExportClientData()
        {
            // 匯出客戶資料 "YYYYMMDD_客戶資料匯出.xlsx"

            byte[] fileContent = GetFileByteArrayFromDB();
            string docTypeStr = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string fileName = dateStr + "_客戶資料匯出.xlsx";

            if (Request.Browser.Browser == "IE" && Convert.ToInt32(Request.Browser.MajorVersion) < 9)
            {
                // 舊版 IE 使用舊的相容性作法
                return File(fileContent, docTypeStr, Server.UrlPathEncode(fileName));
            }
            else
            {
                // 新版瀏覽器使用RFC2231規範的Header Value做法
                return File(fileContent, docTypeStr, fileName);
            }
        }

        private byte[] GetFileByteArrayFromDB()
        {
            var clients = clientRepo.All();
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, clients.ToList());
            return ms.ToArray();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBank(int clientId, int bankId)
        {
            bankRepo.Remove(bankId);
            bankRepo.UnitOfWork.Commit();

            return RedirectToAction("Details", new { id = clientId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContact(int clientId, int contactId)
        {
            contactRepo.Remove(contactId);
            contactRepo.UnitOfWork.Commit();

            return RedirectToAction("Details", new { id = clientId });
        }

        public ActionResult BatchUpdateBank(int clientId, IList<BankBatchUpdateVM> banks)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in banks)
                {
                    var b = bankRepo.FindById(item.Id);
                    b.銀行名稱 = item.銀行名稱;
                    b.銀行代碼 = item.銀行代碼;
                    b.分行代碼 = item.分行代碼;
                    b.帳戶名稱 = item.帳戶名稱;
                    b.帳戶號碼 = item.帳戶號碼;

                    //Mapper.DynamicMap<BankBatchUpdateVM, 客戶銀行資訊>(item, b);
                }
                bankRepo.UnitOfWork.Commit();

                return RedirectToAction("Edit", new { id = clientId });
            }
            return View();
        }

        public ActionResult BatchUpdateContact(int clientId, IList<ContactBatchUpdateVM> contacts)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in contacts)
                {
                    var c = contactRepo.FindById(item.Id);
                    c.職稱 = item.職稱;
                    c.姓名 = item.姓名;
                    c.Email = item.Email;
                    c.手機 = item.手機;
                    c.電話 = item.電話;

                    //Mapper.DynamicMap<ContactBatchUpdateVM, 客戶聯絡人>(item, c);
                }
                contactRepo.UnitOfWork.Commit();

                return RedirectToAction("Edit", new { id = clientId });
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();

                clientRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
