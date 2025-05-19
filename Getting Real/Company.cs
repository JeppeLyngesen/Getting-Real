using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    internal class Company
    {
        private string _name { get; set; }
        private string _email { get; set; }
        private int _phoneNumber { get; set; }
        public Company(string name, string email, int phoneNumber)
        {
            this._name = name;
            this._email = email;
            this._phoneNumber = phoneNumber;
        }
    }
}
