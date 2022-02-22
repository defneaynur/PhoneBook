using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Report
    {
        private string sysTar = DateTime.Now.ToLongDateString();
        private string status = "Hazırlanıyor";


        public int UUID { get; set; }
        public string SysTar 
        {
            get { return sysTar; }
            set { sysTar = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        //public IEnumerable<ContactInformation> ContInfo { get; set; }
        public string Location { get; set; }
        public int LocationContactCount { get; set; }
        public int LocationPhoneCount { get; set; }
    }
}
