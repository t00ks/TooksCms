using System;
using System.Collections.Generic;
using TooksCms.Core.Interfaces;

namespace TooksCms.Core.Interfaces.Repository
{
    public interface IAccountRepository
    {
        bool AddressExists(int id);
        bool ContactExists(int id);
        bool ContactExistsOnUserId(int id);
        bool EmailExists(int id);
        IAddress FetchAddress(int id);
        IEnumerable<IAddress> FetchAddresses();
        IEnumerable<IAddress> FetchAddresses(int count);
        IEnumerable<IAddress> FetchAddresses(int count, int skip);
        IEnumerable<IGuest> FetchAllGuests(int count);
        IEnumerable<IUser> FetchAllUsers(int count);
        IContactInfo FetchContactInfo(int id);
        IContactInfo FetchContactInfoOnUserId(int id);
        IEmail FetchEmail(int id);
        IGuest FetchGuest(int id);
        IGuest FetchGuest(string ipAddress);
        IPhoneNumber FetchPhoneNumber(int id);
        IUser FetchUser(int id);
        IUser FetchUser(string loginName);
        bool GuestExists(int id);
        bool GuestExists(string ipAddress);
        IAddress InsertAddress(IAddress data);
        IContactInfo InsertContact(IContactInfo data);
        IEmail InsertEmail(IEmail data);
        IGuest InsertGuest(IGuest data);
        IPhoneNumber InsertPhoneNumber(IPhoneNumber data);
        IUser InsertUser(IUser data);
        bool PhoneNumberExists(int id);
        IAddress UpdateAddress(IAddress data);
        IContactInfo UpdateContact(IContactInfo data);
        IEmail UpdateEmail(IEmail data);
        IGuest UpdateGuest(IGuest data);
        IPhoneNumber UpdatePhoneNumber(IPhoneNumber data);
        IUser UpdateUser(IUser data);
        bool UserExists(int id);
        bool UserExists(string loginName);
    }
}
