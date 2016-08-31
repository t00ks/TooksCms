using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class EventRepository : IEventRepository
    {
        public void InsertEvent(IEventLog data)
        {
            var db = new TooksCmsDAL();

            var e = EventLog.CreateEventLog(data);
            db.EventLogs.Add(e);

            db.SaveChanges();
        }
    }
}
