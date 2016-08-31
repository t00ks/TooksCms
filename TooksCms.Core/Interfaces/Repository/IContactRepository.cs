using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IContactRepository
    {
        IContactForm FetchContactForm(int id);
        IEnumerable<IContactForm> FetchContactFormList(int count, int skip = 0);
        IContactForm InsertContactForm(IContactForm data);
        IContactForm UpdateContactForm(IContactForm data);
    }
}
