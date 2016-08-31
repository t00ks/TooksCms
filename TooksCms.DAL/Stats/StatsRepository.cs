using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class StatsRepository : IStatsRepository
    {
        #region PageVisit

        public IPageVisit InsertPageVisit(IPageVisit data)
        {
            var db = new TooksCmsDAL();
            var s = PageVisit.CreatePageVisit(data);
            db.PageVisits.Add(s);
            db.SaveChanges();
            return s;
        }

        public IEnumerable<IPageVisit> FetchPageVisits()
        {
            var db = new TooksCmsDAL();
            return db.PageVisits.OrderByDescending(p => p.DateTime).Take(100);
        }

        #endregion

        #region Unique Visits

        public IEnumerable<IUniqueVisit> FetchUniqueVisits(DateTime? from, DateTime? to)
        {
            var db = new TooksCmsDAL();
            return db.GetUniqueVisits(from, to);
        }

        #endregion

        #region Browser Stats
        
        public IEnumerable<IBrowserStat> FetchBrowserStats()
        {
            var db = new TooksCmsDAL();
            return db.GetBrowserStats();
        }

        #endregion
    }
}
