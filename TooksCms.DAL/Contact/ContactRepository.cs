using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.DAL
{
    public class ContactRepository : IContactRepository
    {
        public IContactForm InsertContactForm(IContactForm data)
        {
            var db = new TooksCmsDAL();

            var cf = ContactForm.CreateContactForm(data);
            db.ContactForms.Add(cf);

            db.SaveChanges();

            return cf;
        }

        public IContactForm UpdateContactForm(IContactForm data)
        {
            var db = new TooksCmsDAL();

            if (!db.ContactForms.Any(cf_ => cf_.ContactFormId == data.ContactFormId))
            {
                throw new DataNotFoundException("ContactForm not found in database with id: " + data.ContactFormId.ToString());
            }

            var cf = db.ContactForms.Single(cf_ => cf_.ContactFormId == data.ContactFormId);
            cf.Update(data);

            db.SaveChanges();

            return cf;
        }

        public IContactForm FetchContactForm(int id)
        {
            var db = new TooksCmsDAL();

            if (!db.ContactForms.Any(cf => cf.ContactFormId == id))
            {
                throw new DataNotFoundException("ContactForm not found in database with id: " + id.ToString());
            }

            return db.ContactForms.Single(cf => cf.ContactFormId == id);
        }

        public IEnumerable<IContactForm> FetchContactFormList(int count, int skip = 0)
        {
            var db = new TooksCmsDAL();

            if (count > 0)
            {
                return db.ContactForms.OrderBy(cf => cf.Date).Skip(skip).Take(count);
            }
            return db.ContactForms.OrderBy(cf => cf.Date).Skip(0);
        }
    }
}
