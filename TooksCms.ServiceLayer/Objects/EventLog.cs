using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Enums;

namespace TooksCms.ServiceLayer.Objects
{
    public class EventLog : InterfacingBase, IEventLog
    {
        private EventLog(){}

        public EventLog(IEventLog data)
            : base(data, typeof(IEventLog))
        { }

        public int EventLogId { get; private set; }

        public Guid EventLogUid { get; private set; }

        public EventLogType EventType { get; private set; }

        public string EventSource { get; private set; }

        public string Description { get; private set; }

        public int EventId { get; private set; }

        public static EventLog CreateLog(EventLogType type, string source, string description, int eventId)
        {
            return new EventLog
                       {
                           EventLogUid = Guid.NewGuid(),
                           EventType = type,
                           EventSource = source,
                           Description = description,
                           EventId = eventId
                       };
        }
    }
}
