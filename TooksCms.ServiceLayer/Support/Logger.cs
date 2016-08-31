using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Enums;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.ServiceLayer.Support
{
    public static class Logger
    {
        public static void LogException(EventLogType logType, string source, Exception ex, string method, int id)
        {
            LogMessage(logType, source, GetExceptionMessage(ex, method), id);
        }
        public static void LogMessage(EventLogType logType, string source, string message, int id)
        {
            var rep = DependencyResolver.Current.GetService<IEventRepository>();
            rep.InsertEvent(EventLog.CreateLog(logType, source, message, id));
        }

        public static string GetExceptionMessage(Exception ex, string method)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Method: " + method);
            sb.AppendLine("Message: " + ex.Message);
            sb.AppendLine("Source: " + ex.Source);
            sb.AppendLine("Stack Trace: " + ex.StackTrace);
            sb.AppendLine("*************** INNER Exception**************");
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.AppendLine("Message: " + ex.Message);
                sb.AppendLine("Source: " + ex.Source);
                sb.AppendLine("Stack Trace: " + ex.StackTrace);
            }
            sb.AppendLine("***************END INNER Exception**************");
            return sb.ToString();
        }
    }
}
