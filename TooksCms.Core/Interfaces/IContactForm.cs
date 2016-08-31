using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Interfaces
{
    public interface IContactForm : IInterfacingBase
    {
        int ContactFormId { get; }
        Guid ContactFormUid { get; }
        int SiteId { get; }
        string Title { get; }
        string Name { get; }
        string Email { get; }
        string Content { get; }
        DateTime Date { get; }
        bool Read { get; }
        bool Public { get; }
    }
}
