using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Exceptions;
using TooksCms.Core.Interfaces;
using TooksCms.Core.Interfaces.Repository;

namespace TooksCms.Data
{
    public class AccountRepository : IAccountRepository
    {
        #region User

        /// <summary>
        /// Fetch a single user from the DAL.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>A User DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">User does not exist</exception>
        public IUser FetchUser(int id)
        {
            var db = new Context();

            if (!_userExists(id, db))
            {
                throw new DataNotFoundException("User does not exist", "id");
            }

            return db.Users.FirstOrDefault(u_ => u_.UserId == id);
        }

        /// <summary>
        /// Fetch a single user from the DAL.
        /// </summary>
        /// <param name="id">Login name of the user</param>
        /// <returns>A User DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">User does not exist</exception>
        public IUser FetchUser(string loginName)
        {
            var db = new Context();

            if (!_userExists(loginName, db))
            {
                throw new DataNotFoundException("User not found with login name: " + loginName);
            }

            return db.Users.FirstOrDefault(u_ => u_.LoginName.ToLower() == loginName.ToLower());
        }

        /// <summary>
        /// Fetch list of users from the DAL
        /// </summary>
        /// <param name="count">Number of users to fetch, 0 = All</param>
        /// <returns>An enumerable collection of User DTO objects</returns>
        public IEnumerable<IUser> FetchAllUsers(int count)
        {
            var db = new Context();

            return count == 0 ? db.Users : db.Users.Take(count);
        }

        /// <summary>
        /// Inserts a new user into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An User DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IUser InsertUser(IUser data)
        {
            var db = new Context();
            var u = User.CreateUser(data);

            db.Users.Add(u);

            db.SaveChanges();

            return u;
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="data">User to update</param>
        /// <returns>An User DTO object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">User does not exist</exception>
        public IUser UpdateUser(IUser data)
        {
            var db = new Context();

            if (!_userExists(data.UserId, db))
            {
                throw new DataNotFoundException("User does not exists in the DAL", "id");
            }

            var u = db.Users.FirstOrDefault(u_ => u_.UserId == data.UserId);
            db.U .Update(data);

            db.SaveChanges();

            return u;
        }

        /// <summary>
        /// Checks the existance of a user in the DAL
        /// </summary>
        /// <param name="id">ID of User object to check for</param>
        /// <returns>true or false</returns>
        public bool UserExists(int id)
        {
            var db = new Context();
            return _userExists(id, db);
        }

        /// <summary>
        /// Checks the existance of a user in the DAL
        /// </summary>
        /// <param name="id">Login name of User object to check for</param>
        /// <returns>true or false</returns>
        public bool UserExists(string loginName)
        {
            var db = new Context();
            return _userExists(loginName, db);
        }

        private bool _userExists(int id, Context db)
        {
            return db.Users.Any(u_ => u_.UserId == id);
        }

        private bool _userExists(string loginName, Context db)
        {
            return db.Users.Any(u_ => u_.LoginName.ToLower() == loginName.ToLower());
        }

        #endregion

        #region ContactInfo

        /// <summary>
        /// Fetch a single contact info from the DAL.
        /// </summary>
        /// <param name="id">ID of the contact</param>
        /// <returns>A ContactInfo DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">ContactInfo does not exist</exception>
        public IContactInfo FetchContactInfo(int id)
        {
            var db = new Context();

            if (!_contactExists(id, db))
            {
                throw new DataNotFoundException("Contact does not exist", "id");
            }

            return db.ContactInfoes.FirstOrDefault(c_ => c_.ContactInfoId == id);
        }

        /// <summary>
        /// Fetch a single contact info from the DAL.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>A ContactInfo DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Contact does not exist</exception>
        public IContactInfo FetchContactInfoOnUserId(int id)
        {
            var db = new Context();

            if (!_contactExistsOnUserId(id, db))
            {
                throw new DataNotFoundException("Contact does not exist", "id");
            }

            return db.ContactInfoes.FirstOrDefault(c_ => c_.User.UserId == id);
        }

        /// <summary>
        /// Inserts a new contact info into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An ContactInfo DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IContactInfo InsertContact(IContactInfo data)
        {
            var db = new Context();
            var contactInfo = ContactInfo.CreateContactInfo(data);

            db.ContactInfoes.Add(contactInfo);

            db.SaveChanges();

            return contactInfo;
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="data">ContactInfo to update</param>
        /// <returns>An ContactInfo DTO object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">ContactInfo does not exist</exception>
        public IContactInfo UpdateContact(IContactInfo data)
        {
            var db = new Context();

            if (!_contactExists(data.ContactInfoId, db))
            {
                throw new DataNotFoundException("ContactInfo does not exits", "id");
            }

            var ci = db.ContactInfoes.First(ci_ => ci_.ContactInfoId == data.ContactInfoId);
            ci.Update(data);

            db.SaveChanges();

            return ci;
        }

        /// <summary>
        /// Checks the existance of a contact in the DAL
        /// </summary>
        /// <param name="id">ID of Contact object to check for</param>
        /// <returns>true or false</returns>
        public bool ContactExists(int id)
        {
            var db = new Context();
            return _contactExists(id, db);
        }

        /// <summary>
        /// Checks the existance of a contact in the DAL
        /// </summary>
        /// <param name="id">ID of User object assosiated to Contact</param>
        /// <returns>true or false</returns>
        public bool ContactExistsOnUserId(int id)
        {
            var db = new Context();
            return _contactExistsOnUserId(id, db);
        }

        private bool _contactExists(int id, Context db)
        {
            return db.ContactInfoes.Any(c_ => c_.ContactInfoId == id);
        }

        private bool _contactExistsOnUserId(int id, Context db)
        {
            return db.ContactInfoes.Any(c_ => c_.UserId == id);
        }

        #endregion

        #region Address

        public IAddress FetchAddress(int id)
        {
            var db = new Context();

            if (!_addressExists(id, db))
            {
                throw new DataNotFoundException("Address does not exists in DAL", "id");
            }

            var a = db.Addresses.FirstOrDefault(a_ => a_.AddressId == id);

            return a;

        }

        public IEnumerable<IAddress> FetchAddresses()
        {
            var db = new Context();
            return db.Addresses.ToList();
        }

        public IEnumerable<IAddress> FetchAddresses(int count)
        {
            var db = new Context();
            return db.Addresses.Take(count).ToList();
        }

        public IEnumerable<IAddress> FetchAddresses(int count, int skip)
        {
            var db = new Context();
            return db.Addresses.Skip(skip).Take(count).ToList();
        }

        /// <summary>
        /// Inserts a new address into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An Address DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IAddress InsertAddress(IAddress data)
        {
            var db = new Context();
            var address = Address.CreateAddress(data);

            db.Addresses.Add(address);

            db.SaveChanges();

            return address;
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="data">Address to update</param>
        /// <returns>An Address DTO object</returns>
        public IAddress UpdateAddress(IAddress data)
        {
            var db = new Context();

            if (!_addressExists(data.AddressId, db))
            {
                throw new DataNotFoundException("Article does not exits", "id");
            }

            var a = db.Addresses.First(a_ => a_.AddressId == data.AddressId);
            a.Update(data);

            db.SaveChanges();

            return a;
        }

        /// <summary>
        /// Checks the existance of a address in the DAL
        /// </summary>
        /// <param name="id">ID of Address object to check for</param>
        /// <returns>true or false</returns>
        public bool AddressExists(int id)
        {
            var db = new Context();
            return _userExists(id, db);
        }

        private bool _addressExists(int id, Context db)
        {
            return db.Addresses.Any(a_ => a_.AddressId == id);
        }

        #endregion

        #region PhoneNumber

        /// <summary>
        /// Fetch a single user from the DAL.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>A User DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">User does not exist</exception>
        public IPhoneNumber FetchPhoneNumber(int id)
        {
            var db = new Context();

            if (!_phoneNumberExists(id, db))
            {
                throw new DataNotFoundException("PhoneNumber does not exist", "id");
            }

            return db.PhoneNumbers.FirstOrDefault(pn_ => pn_.PhoneNumberId == id);
        }

        /// <summary>
        /// Inserts a new phone number into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An PhoneNumber DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IPhoneNumber InsertPhoneNumber(IPhoneNumber data)
        {
            var db = new Context();

            var pn = PhoneNumber.CreatePhoneNumber(data);

            db.PhoneNumbers.Add(pn);

            db.SaveChanges();

            return pn;
        }

        /// <summary>
        /// Updates an existing phone number.
        /// </summary>
        /// <param name="data">PhoneNumber to update</param>
        /// <returns>An PhoneNumber DTO object</returns>
        public IPhoneNumber UpdatePhoneNumber(IPhoneNumber data)
        {
            var db = new Context();

            if (!_addressExists(data.PhoneNumberId, db))
            {
                throw new DataNotFoundException("PhoneNumber does not exits", "id");
            }

            var pn = db.PhoneNumbers.First(pn_ => pn_.PhoneNumberId == data.PhoneNumberId);
            pn.Update(data);

            db.SaveChanges();

            return pn;
        }

        /// <summary>
        /// Checks the existance of a phone number in the DAL
        /// </summary>
        /// <param name="id">ID of PhoneNumber object to check for</param>
        /// <returns>true or false</returns>
        public bool PhoneNumberExists(int id)
        {
            var db = new Context();
            return _phoneNumberExists(id, db);
        }

        private bool _phoneNumberExists(int id, Context db)
        {
            return db.PhoneNumbers.Any(pn_ => pn_.PhoneNumberId == id);
        }

        #endregion

        #region Email

        /// <summary>
        /// Fetch a email user from the DAL.
        /// </summary>
        /// <param name="id">ID of the email</param>
        /// <returns>A Email DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Email does not exist</exception>
        public IEmail FetchEmail(int id)
        {
            var db = new Context();

            if (!_emailExists(id, db))
            {
                throw new DataNotFoundException("Email does not exist in DAL", "ID");
            }

            return db.Emails.FirstOrDefault(e_ => e_.EmailId == id);
        }

        /// <summary>
        /// Inserts a new email into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An Email DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IEmail InsertEmail(IEmail data)
        {
            var db = new Context();

            var e = Email.CreateEmail(data);

            db.Emails.Add(e);

            db.SaveChanges();

            return e;
        }

        /// <summary>
        /// Updates an existing email.
        /// </summary>
        /// <param name="data">Email to update</param>
        /// <returns>An Email DAL object</returns>
        public IEmail UpdateEmail(IEmail data)
        {
            var db = new Context();

            if (!_emailExists(data.EmailId, db))
            {
                throw new DataNotFoundException("Email does not exist in DAL", "ID");
            }
            var e = db.Emails.FirstOrDefault(e_ => e_.EmailId == data.EmailId);

            e.Update(data);

            db.SaveChanges();

            return e;
        }

        /// <summary>
        /// Checks the existance of a email in the DAL
        /// </summary>
        /// <param name="id">ID of Email object to check for</param>
        /// <returns>true or false</returns>
        public bool EmailExists(int id)
        {
            var db = new Context();

            return _emailExists(id, db);
        }

        private bool _emailExists(int id, Context db)
        {
            return db.Emails.Any(e_ => e_.EmailId == id);
        }
        #endregion

        #region Guest

        /// <summary>
        /// Fetch a single guest from the DAL.
        /// </summary>
        /// <param name="id">ID of the guest</param>
        /// <returns>A Guest DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Guest does not exist</exception>
        public IGuest FetchGuest(int id)
        {
            var db = new Context();

            if (!_guestExists(id, db))
            {
                throw new DataNotFoundException("Guest does not exist", "id");
            }

            return db.Guests.FirstOrDefault(g_ => g_.GuestId == id);
        }

        /// <summary>
        /// Fetch a single guest from the DAL.
        /// </summary>
        /// <param name="id">IP Address of the guest</param>
        /// <returns>A Guest DAL object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Guest does not exist</exception>
        public IGuest FetchGuest(string ipAddress)
        {
            var db = new Context();

            //if (!_guestExists(ipAddress, db))
            //{
            //    throw new DataNotFoundException("Guest not found with IP address: " + ipAddress);
            //}

            return db.Guests.FirstOrDefault(g_ => g_.IpAddress.ToLower() == ipAddress.ToLower() && !g_.IsArchived);
        }

        /// <summary>
        /// Fetch list of guests from the DAL
        /// </summary>
        /// <param name="count">Number of guets to fetch, 0 = All</param>
        /// <returns>An enumerable collection of Guest DTO objects</returns>
        public IEnumerable<IGuest> FetchAllGuests(int count)
        {
            var db = new Context();

            return count == 0 ? db.Guests : db.Guests.Take(count);
        }

        /// <summary>
        /// Inserts a new guest into the DAL.
        /// </summary>
        /// <param name="data">DTO to create from</param>
        /// <returns>An Guest DAL object</returns>
        /// <exception cref="System.ArgumentException">Errors in data will result in an exception being thrown</exception>
        public IGuest InsertGuest(IGuest data)
        {
            var db = new Context();
            var g = Guest.CreateGuest(data);

            db.Guests.Add(g);

            db.SaveChanges();

            return g;
        }

        /// <summary>
        /// Updates an existing guest.
        /// </summary>
        /// <param name="data">Guest to update</param>
        /// <returns>An User DTO object</returns>
        /// <exception cref="TooksCms.Core.Exceptions.DataNotFoundException">Guest does not exist</exception>
        public IGuest UpdateGuest(IGuest data)
        {
            var db = new Context();

            if (!_guestExists(data.GuestId, db))
            {
                throw new DataNotFoundException("Guest does not exists in the DAL", "id");
            }

            var g = db.Guests.FirstOrDefault(g_ => g_.GuestId == data.GuestId);
            g.Update(data);

            db.SaveChanges();

            return g;
        }

        /// <summary>
        /// Checks the existance of a guest in the DAL
        /// </summary>
        /// <param name="id">ID of Guest object to check for</param>
        /// <returns>true or false</returns>
        public bool GuestExists(int id)
        {
            var db = new Context();
            return _guestExists(id, db);
        }

        /// <summary>
        /// Checks the existance of a guest in the DAL
        /// </summary>
        /// <param name="id">Login name of Guest object to check for</param>
        /// <returns>true or false</returns>
        public bool GuestExists(string ipAddress)
        {
            var db = new Context();
            return _guestExists(ipAddress, db);
        }

        private bool _guestExists(int id, Context db)
        {
            return db.Guests.Any(g_ => g_.GuestId == id);
        }

        private bool _guestExists(string ipAddress, Context db)
        {
            return db.Guests.Any(g_ => g_.IpAddress.ToLower() == ipAddress.ToLower());
        }

        #endregion
    }
}
