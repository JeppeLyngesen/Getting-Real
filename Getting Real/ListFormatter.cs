using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public static class ListFormatter
    {
        public static void PrintTimeslotsAsTable(List<DateTime> timeslots) //mulighed, men er ikke tilfreds
        {
            var grouped = timeslots
                .GroupBy(t => t.Date)
                .OrderBy(g => g.Key);

            Console.WriteLine("Ledige Tider:\n");

            foreach (var group in grouped)
            {
                Console.Write($"{group.Key:dd - MM - yyyy}:\t");

                var times = group.OrderBy(t => t.TimeOfDay)
                    .Select(t => t.ToString("HH:mm"));

                Console.WriteLine(string.Join("   ", times));
            }
        }
    }
}
