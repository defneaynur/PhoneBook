using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class ContactWithContactInformation
    {
        public int UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public IEnumerable<ContactInformation> ContInfo { get; set; }
    }
}
