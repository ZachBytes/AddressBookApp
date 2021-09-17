using AddressBookApp.Contexts;
using AddressBookApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AddressBookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsAPIController : ControllerBase
    {
        private readonly AddressDbContext _context;

        public ContactsAPIController(AddressDbContext context)
        {
            _context = context;
        }
        // GET: api/<ContactsController>
        [HttpGet]
        public ActionResult<List<Contact>> GetContacts()
        {


            var contacts = _context.Contacts
                .Include(x => x.Addresses)
                .ToList();
            return contacts;


        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactsController>
        [HttpPost]
        public void CreateContact([FromBody] ContactModel jsonContact)
        {
            //var jsonContact = JsonConvert.DeserializeObject<ContactModel>(value);
            var contact = new Contact { FirstName = jsonContact.FirstName, LastName = jsonContact.LastName };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public void EditContact(int id, [FromBody] ContactModel jsonContact)
        {
            Contact updatedContact = (from c in _context.Contacts
                                      where c.ContactID == id
                                      select c).FirstOrDefault();

            if (updatedContact != null)
            {
                updatedContact.FirstName = jsonContact.FirstName;
                updatedContact.LastName = jsonContact.LastName;

                _context.SaveChanges();
            }
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _context.Contacts.Where(item => item.ContactID == id).Single();
            _context.Contacts.Remove(item);
            _context.SaveChanges();
        }


    }
}
