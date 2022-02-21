using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class ContactInformation
    {
        public ObjectId Id { get; set; }
        public int InfoId { get; set; }
        public int ContactUUID { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
    }
}
