using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class Booking
    {
        private DateTime _time {  get; set; }
        private string _address { get; set; }

        public Booking(DateTime time, string address)
        {
            this._time = time;
            this._address = address;
        }

        public override string ToString()
        {
            return $"{_time.ToString("dd-MM-yyyy HH:mm")};{_address}";
        }

        public static Booking FromString(string input)
        {
            var parts = input.Split(';');

            if (parts.Length != 2)
            {
                throw new FormatException("Input needs exactly one semicolon seperator");
            }

            if (!DateTime.TryParseExact(parts[0], "dd-MM-yyyy HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime time))
            {
                throw new FormatException("Time was not expected format: dd-MM-yyyy HH:mm");
            }

            string address = parts[1];

            return new Booking(time, address);
        }
    }
}
