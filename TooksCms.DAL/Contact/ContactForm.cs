using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;

namespace TooksCms.DAL
{
    public partial class ContactForm : IContactForm
    {
        public static ContactForm CreateContactForm(IContactForm data)
        {
            return new ContactForm
            {
                ContactFormUid = data.ContactFormUid,
                SiteId = data.SiteId,
                Title = data.Title,
                Name = data.Name,
                Email = data.Email,
                Content = data.Content,
                Read = data.Read,
                Public = data.Public, 
                Date = data.Date
            };
        }

        public void Update(IContactForm data)
        {
            this.Public = data.Public;
            this.Read = data.Read;
        }
    }
}
