﻿
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ContactManager.Models;
using System.Collections;

namespace ContactManager.Models
{
    public class EntityContactManagerRepository :  IContactManagerRepository

    {
        private ContactManagerDBEntities _entities = new ContactManagerDBEntities();

        public Contact GetContact(int id)
        {
            return (from c in _entities.Contacts
                    where c.Id == id
                    select c).FirstOrDefault();
        }

        public IEnumerable<Contact> ListContacts()
        {
            return _entities.Contacts.ToList();
        }

        public Contact CreateContact(Contact contactToCreate)
        {
            _entities.Entry(contactToCreate).State = EntityState.Added;
            _entities.SaveChanges();
            return contactToCreate;
        }

        public Contact EditContact(Contact contactToEdit)
        {
            var originalContact = GetContact(contactToEdit.Id);
            _entities.Entry(originalContact).State = EntityState.Modified;
            _entities.SaveChanges();
            return contactToEdit;
        }

        public void DeleteContact(Contact contactToDelete)
        {
            var originalContact = GetContact(contactToDelete.Id);
            _entities.Entry(originalContact).State = EntityState.Deleted;
            _entities.SaveChanges();
        }

     
    }
}