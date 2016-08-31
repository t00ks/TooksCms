using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;

namespace TooksCms.DAL
{
    public partial class EventLog : IEventLog
    {
        public static EventLog CreateEventLog(IEventLog data)
        {
            return new EventLog
            {
                EventLogUid = data.EventLogUid,
                EventType = (byte)data.EventType,
                EventSource = data.EventSource,
                Description = data.Description,
                EventId = data.EventId
            };
        }


        EventLogType IEventLog.EventType
        {
            get
            {
                return (EventLogType)this.EventType; 
            }
        }
    }
}
