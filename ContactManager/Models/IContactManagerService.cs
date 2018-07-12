using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContactManager.Models
{
    public interface IContactManagerService
    {
        bool CreateContact(Contact contactToCreate);
        bool DeleteContact(Contact contactToDelete);
        bool EditContact(Contact contactToEdit);
        Contact GetContact(int id);
        IEnumerable<Contact> ListContacts();
    }
}