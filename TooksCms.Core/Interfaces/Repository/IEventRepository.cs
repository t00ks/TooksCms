using System;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IEventRepository
    {
        void InsertEvent(IEventLog data);
    }
}
