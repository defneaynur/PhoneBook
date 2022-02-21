using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Contacts
    {
        public ObjectId Id { get; set; }
        [Key]
        public int UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        //public ContactInformation ContactInfo { get; set; }

    }
}
