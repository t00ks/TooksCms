using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects
{
    public class ContactForm : InterfacingBase, IContactForm
    {
        private ContactForm() { }

        public ContactForm(IContactForm data) :
            base(data, typeof(IContactForm)) { }

        #region IContactForm Implementation

        public int ContactFormId { get; private set; }

        public Guid ContactFormUid { get; private set; }

        public int SiteId { get; private set; }

        public string Title { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Content { get; private set; }

        public DateTime Date { get; private set; }

        public bool Read { get; private set; }

        public bool Public { get; private set; }

        #endregion

        public static ContactForm CreateContactForm(int id, Guid uid, int siteId, string title, string name,
            string email, string content, DateTime date, bool read, bool @public)
        {
            return new ContactForm
            {
                ContactFormId = id,
                ContactFormUid = uid,
                SiteId = siteId,
                Title = title,
                Name = name,
                Email = email,
                Content = content,
                Read = read,
                Public = @public,
                Date = date
            };
        }
    }
}
