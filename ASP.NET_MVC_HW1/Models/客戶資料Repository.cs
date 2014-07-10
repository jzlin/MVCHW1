using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ASP.NET_MVC_HW1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.是否已刪除 == false);
        }

        public 客戶資料 FindById(int id)
        {
            return All().Where(p => p.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            客戶資料 客戶資料 = this.FindById(id);
            客戶資料.是否已刪除 = true;
        }

	}

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}