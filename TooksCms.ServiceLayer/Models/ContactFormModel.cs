using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooksCms.Core.Bases;
using System.ComponentModel.DataAnnotations;
using TooksCms.Core.Interfaces;
using TooksCms.ServiceLayer.Objects;
using TooksCms.Core.Interfaces.Repository;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using TooksCms.Core.Mail;
using System.Configuration;
using TooksCms.ServiceLayer.Support;
using TooksCms.ServiceLayer.Utilities;

namespace TooksCms.ServiceLayer.Models
{
    public class ContactFormModel : ModelBase
    {
        [Dependency]
        private IContactRepository _contactRepository { get; set; }

        public ContactFormModel() { }

        public ContactFormModel(IContactForm data)
        {
            this.Id = data.ContactFormId;
            this.Uid = data.ContactFormUid;
            this.Title = data.Title;
            this.Name = data.Name;
            this.Email = data.Email;
            this.Comment = data.Content;
            this.Read = data.Read;
            this.Public = data.Public;
        }

        [Required]
        [Display(Name = "Subject", Prompt = "Please give your comment a subject.")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Name", Prompt = "Please enter a name.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email", Prompt = "Please enter a contact e-mail address")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Comment", Prompt = "Please enter your comment.")]
        [DataType(DataType.Text)]
        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public bool Read { get; set; }

        public bool Public { get; set; }

        #region CRUD

        public void Save()
        {
            _contactRepository = DependencyResolver.Current.GetService<IContactRepository>();
            try
            {
                if (!IsNew & IsDeleted)
                {
                    /* [Delete] an existing object marked for deletion */
                    //dc.ArticleDelete(Id);
                }
                else
                {
                    /* Exception will cause the transaction to rollback */
                    if (IsNew)
                    {
                        /* [Insert] a new and valid object to be saved */
                        _contactRepository.InsertContactForm(BuildInterface());
                    }
                    else if (!IsNew & IsDirty)
                    {
                        /* [Update] a existing, but changed object to be saved */
                        _contactRepository.UpdateContactForm(BuildInterface());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Notifier.SendNotification(this);
        }

        private IContactForm BuildInterface()
        {
            return ContactForm.CreateContactForm(this.Id, this.Uid, 1, this.Title, this.Name, this.Email, this.Comment, this.Date, this.Read, this.Public);
        }
        
        #endregion

        #region Factory

        public static List<ContactFormModel> GetList()
        {
            var cRep = DependencyResolver.Current.GetService<IContactRepository>();
            return cRep.FetchContactFormList(0).Select(c_ => new ContactFormModel(c_)).ToList();
        }

        public static ContactFormModel Load(int id)
        {
            var cRep = DependencyResolver.Current.GetService<IContactRepository>();
            return new ContactFormModel(cRep.FetchContactForm(id));
        }

        #endregion
    }
}
