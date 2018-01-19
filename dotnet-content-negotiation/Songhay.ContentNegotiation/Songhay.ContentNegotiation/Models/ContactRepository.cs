using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Songhay.ContentNegotiation.Models
{
    public class ContactRepository : IContactRepository
    {
        static ContactRepository() => contacts = new ConcurrentDictionary<string, Contact>();

        public ContactRepository()
        {
            Add(new Contact() { FirstName = "Nancy", LastName = "Davolio" });
        }

        public void Add(Contact contact)
        {
            contact.ID = Guid.NewGuid().ToString();
            contacts[contact.ID] = contact;
        }

        public Contact Get(string id)
        {
            contacts.TryGetValue(id, out Contact contact);
            return contact;
        }

        public IEnumerable<Contact> GetAll()
        {
            return contacts.Values;
        }

        public Contact Remove(string id)
        {
            contacts.TryRemove(id, out Contact contact);
            return contact;
        }

        public void Update(Contact contact)
        {
            contacts[contact.ID] = contact;
        }

        static readonly ConcurrentDictionary<string, Contact> contacts;
    }
}
