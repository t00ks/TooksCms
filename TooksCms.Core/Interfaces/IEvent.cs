using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Enums;

namespace TooksCms.Core.Interfaces
{
    public interface IEventLog : IInterfacingBase
    {
        int EventLogId { get; }
        Guid EventLogUid { get; }
        EventLogType EventType { get; }
        string EventSource { get; }
        string Description { get; }
        int EventId { get; }
    }
}
