using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ASP.NET_MVC_HW1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {

        public override IQueryable<客戶聯絡人> All()
        {
             return base.All().Where(p => p.是否已刪除 == false).Where(p => p.客戶資料.是否已刪除 == false);
        }

        public 客戶聯絡人 FindById(int id)
        {
            return All().Where(p => p.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            客戶聯絡人 客戶聯絡人 = this.FindById(id);
            if (客戶聯絡人 != null)
            {
                客戶聯絡人.是否已刪除 = true;
            }
        }

	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}