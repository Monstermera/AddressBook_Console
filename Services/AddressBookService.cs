using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Models;

namespace AddressBook.Services
{
   public class AddressBookService
    {
        public List<Contact> ContactList { get; set; } = new List<Contact>();
    }
}
