using System;
using System.Linq;
using System.Collections.Generic;
	
namespace ASP.NET_MVC_HW1.Models
{   
	public  class SimpleReportRepository : EFRepository<SimpleReport>, ISimpleReportRepository
    {

        private 客戶資料Repository clientRepo = RepositoryHelper.Get客戶資料Repository();

        public override IQueryable<SimpleReport> All()
        {
            var reports = base.All();

            foreach (SimpleReport item in reports.ToList())
            {
                var client = clientRepo.FindById(item.Id);
                if (client.是否已刪除 == true)
                {
                    reports = reports.Where(p => p.Id != client.Id);
                }
            }

            return reports;
        }

        public SimpleReport FindById(int id)
        {
            return All().Where(p => p.Id == id).SingleOrDefault();
        }

	}

	public  interface ISimpleReportRepository : IRepository<SimpleReport>
	{

	}
}