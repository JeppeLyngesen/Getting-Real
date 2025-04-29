using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
 I mockdataen er filen:
    - "timeslots" alle MULIGE booking tider. 
    - "company_cars" er nye vognløbsnummer, virksomhed, tidligere vognløbsnummer. 
      nyt nummer hedder 6xxx og gammelt nummer 5xxx for debugging.
    - "bookings" er aktuelle bookinger og skal i metoderne sammenholde de to, for at se frie bookinger.
      Den indeholder ved hver booking BookingID, VognId, Selskabsnavn, Email, Telefonnummer og DatoTid.

*/

namespace Getting_Real
{
    public class Datahandler
    {
        private readonly string _bookingsFile = @"Data/mock_bookings.txt";
        private readonly string _companiesFile = @"Data/mock_company_cars.txt";
        private readonly string _timeslotsFile = @"Data/mock_timeslots.txt";

        public List<string> LoadBookings()
        {
            return File.Exists(_bookingsFile) ? new List<string>(File.ReadAllLines(_bookingsFile)) : new List<string>();
        }

        public List<string> LoadCompanies()
        {
            return File.Exists(_companiesFile) ? new List<string>(File.ReadAllLines(_companiesFile)) : new List<string>();
        }

        public List<string> LoadTimeslots()
        {
            return File.Exists(_timeslotsFile) ? new List<string>(File.ReadAllLines(_timeslotsFile)) : new List<string>();
        }

        public void SaveBookings(List<string> lines)
        {
            File.WriteAllLines(_bookingsFile, lines);
        }

        public void SaveCompanies(List<string> lines)
        {
            File.WriteAllLines(_companiesFile, lines);
        }

        public void SaveTimeslots(List<string> lines)
        {
            File.WriteAllLines(_timeslotsFile, lines);
        }
    }
}



