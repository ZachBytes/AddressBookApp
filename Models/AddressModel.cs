using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApp.Models
{
    public class AddressModel
    {

        public int AddressID { get; set; }
        public int ContactID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public string EditAddress { get; set; }
        public string DeleteAddress { get; set; }
    }

}
