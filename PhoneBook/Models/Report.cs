using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class Report
    {
        private DateTime sysTar = DateTime.Now;
        private string status = "Hazırlanıyor";


        public int UUID { get; set; }
        public DateTime SysTar 
        {
            get { return sysTar; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Location { get; set; }
        public int LocationContactCount { get; set; }
        public int LocationPhoneCount { get; set; }
    }
}
