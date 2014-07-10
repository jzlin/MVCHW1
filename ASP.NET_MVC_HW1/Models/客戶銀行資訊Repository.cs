using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ASP.NET_MVC_HW1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {

        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.是否已刪除 == false).Where(p => p.客戶資料.是否已刪除 == false);
        }

        public 客戶銀行資訊 FindById(int id)
        {
            return All().Where(p => p.Id == id).SingleOrDefault();
        }

        public void Remove(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = this.FindById(id);
            客戶銀行資訊.是否已刪除 = true;
        }

	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}