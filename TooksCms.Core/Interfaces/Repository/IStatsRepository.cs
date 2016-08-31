using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IStatsRepository
    {
        IPageVisit InsertPageVisit(IPageVisit data);
        IEnumerable<IPageVisit> FetchPageVisits();
        IEnumerable<IUniqueVisit> FetchUniqueVisits(DateTime? from, DateTime? to);
        IEnumerable<IBrowserStat> FetchBrowserStats();
    }
}
