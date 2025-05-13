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

        public override string ToString()
        {
            return $"{_name};{_email};{_phoneNumber}";
        }

        public static Company FromString(string input)
        {
            var parts = input.Split(";");

            if (parts.Length != 3)
            {
                throw new FormatException("Input needs exactly one semicolon seperator");
            }

            string name = parts[0];
            string email = parts[1];

            if (!int.TryParse(parts[2], out int phoneNumber))
            {
                throw new FormatException("phoneNumber is not an integer");
            }

            return new Company(name, email, phoneNumber);
        }
    }
}
