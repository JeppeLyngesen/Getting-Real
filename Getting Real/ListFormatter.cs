using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public static class ListFormatter
    {
        public static void PrintTimeslotsAsTable(List<DateTime> timeslots)
        {
            var grouped = timeslots
                .GroupBy(t => t.Date)
                .OrderBy(g => g.Key);

            Console.WriteLine("Ledige Tider:\n");

            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key:dd - MM - yyyy}:\t");

                var times = group.OrderBy(t => t.TimeOfDay)
                    .Select(t => t.ToString("HH:mm"));

                Console.WriteLine(string.Join("   ", times));
                Console.WriteLine(" ");
            }
        }

        public static DateTime? PromptUserToSelectTimeslot(List<DateTime> timeslots)
        {
            const int pageSize = 10;
            var ordered = timeslots.OrderBy(t => t).ToList();
            int currentPage = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Vælg en ledig tid ---\n");

                int start = currentPage * pageSize;
                int end = Math.Min(start + pageSize, ordered.Count);

                for (int i = start; i < end; i++)
                {
                    Console.WriteLine($"{i + 1}. {ordered[i]:dd-MM-yyyy HH:mm}");
                }

                Console.WriteLine("\n(d = næste side, a = forrige, w = annullér)");
                Console.Write("Indtast nummer på ønsket booking tid, for at booke: ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "w")
                    return null;
                if (input == "d" && end < ordered.Count)
                {
                    currentPage++;
                    continue;
                }
                if (input == "a" && currentPage > 0)
                {
                    currentPage--;
                    continue;
                }
                if (int.TryParse(input, out int choice) &&
                    choice >= 1 && choice <= ordered.Count)
                {
                    return ordered[choice - 1];
                }

                Console.WriteLine("Ugyldigt input. Tryk en tast for at prøve igen.");
                Console.ReadKey();
            }
        }

        public static void PrintBookingsWithCarID(List<string> bookings)

        {
            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("Ingen bookinger at vise");
                return;
            }

            var bookingData = bookings
                .Select(line => line.Split(';'))
                .Where(parts => parts.Length >= 6) // fix: >= i stedet for > 6
                .Select(parts => new
                {
                    Time = DateTime.Parse(parts[5]),
                    CarID = parts[1],
                })
                .OrderBy(b => b.Time)
                .ToList();

            var grouped = bookingData.GroupBy(b => b.Time.Date);

            Console.WriteLine("Dine bookinger:\n");

            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key:dd-MM-yyyy}:\t");

                foreach (var booking in group.OrderBy(b => b.Time.TimeOfDay))
                {
                    Console.WriteLine($"  {booking.Time:HH:mm}  -  Vogn: {booking.CarID}");
                }

                Console.WriteLine();
            }
        }


    }
}
/* public static void PrintTimeslotsAsTable(List<DateTime> timeslots) //mulighed, men er ikke tilfreds
        {
            var grouped = timeslots
                .GroupBy(t => t.Date)
                .OrderBy(g => g.Key);

           
 Console.WriteLine("Ledige Tider:\n");
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key:dd - MM - yyyy}:\t");
                
                var times = group.OrderBy(t => t.TimeOfDay)
                    .Select(t => t.ToString("HH:mm"));

                Console.WriteLine(string.Join("   ", times));
                Console.WriteLine(" ");
            }
        }

*/