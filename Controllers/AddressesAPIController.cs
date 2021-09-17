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
    public class AddressesAPIController : ControllerBase
    {
        private readonly AddressDbContext _context;

        public AddressesAPIController(AddressDbContext context)
        {
            _context = context;
        }
        // GET: api/<AddressesController>
        [HttpGet("{id}")]
        public ActionResult<List<AddressModel>> GetAddresses(int id)
        {
            var addressesList = new List<AddressModel>();

            var addresses = _context.Addresses
                .Where(x => x.ContactID == id)
                .ToList();

            foreach (var address in addresses)
            {
                addressesList.Add(new AddressModel
                {
                    AddressID = address.AddressID,
                    ContactID = address.ContactID,
                    StreetAddress = address.StreetAddress,
                    City = address.City,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    EditAddress = $"<a href='#' class='btn btn-dark' onclick='openEditAddressModal({address.AddressID})'>Edit Address</a>",
                    DeleteAddress = $"<a href='#' class='btn btn-dark' onclick='deleteAddress({address.AddressID})'>Delete Address</a>"
                });
            }
            return addressesList;


        }

        // GET api/<AddressesController>/5
        //[HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AddressesController>
        [HttpPost]
        public void CreateAddress([FromBody] Address jsonAddress)
        {

            _context.Addresses.Add(jsonAddress);
            _context.SaveChanges();
        }

        // PUT api/<AddressesController>/5
        [HttpPut("{id}")]
        public void EditAddress(int id, [FromBody] Address jsonAddress)
        {
            Address updatedAddress = (from a in _context.Addresses
                                      where a.AddressID == id
                                      select a).FirstOrDefault();

            if (updatedAddress != null)
            {
                updatedAddress.StreetAddress = jsonAddress.StreetAddress;
                updatedAddress.City = jsonAddress.City;
                updatedAddress.State = jsonAddress.State;
                updatedAddress.PostalCode = jsonAddress.PostalCode;

                _context.SaveChanges();
            }
        }

        // DELETE api/<AddressesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var item = _context.Addresses.Where(item => item.AddressID == id).Single();
            _context.Addresses.Remove(item);
            _context.SaveChanges();

            //var address = await _context.Addresses.FindAsync(id);
            //_context.Addresses.Remove(address);
            //await _context.SaveChangesAsync();
        }


    }
}
