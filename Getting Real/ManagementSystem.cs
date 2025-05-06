using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Getting_Real
{
    public class ManagementSystem
    {
        public void Start()
        {

            // Main Menu
            string title = "--- Booking af vognkontrol hos Sydtrafik ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] MainMenuItems = { "Bookingmenu", "Info om vognkontrol", "Opret bruger", "Afslut" };
            MenuDisplay mainMenu = new MenuDisplay(title, MainMenuItems);
            int indexChosen = mainMenu.Run();

            switch (indexChosen)
            {
                case 0: // Bookingmenu - book appointment, view/ change/ cancel booking
                    ChooseUserType();
                    break;
                case 1: // Info about car inspection
                    CarInspectionInfo();
                    break;
                case 2: // Register user
                    RegisterUser(); // Functionality to be implemented
                    break;
                case 3: // Exit
                    Exit();
                    return;
            }

        }

        private void ChooseUserType()
        {

            string title = "--- Vælg din brugertype ---\n\nVælg ønsket brugertype og tast enter.\n";
            string[] userTypeMenuItems = { "Vognmand", "Kontrollør", "Afslut" };
            MenuDisplay userTypeMenu = new MenuDisplay(title, userTypeMenuItems);
            int userTypeChoice = userTypeMenu.Run();

            switch (userTypeChoice)
            {
                case 0:
                    LoginDriver("vognmand");
                    break;
                case 1:
                    LoginInspector("kontrollør");
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        private void LoginDriver(string userTypeChoice)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast adgangskode: ");
            string password = Console.ReadLine();
            Console.ReadKey();

            RunDriverMenu();



        }

        private void LoginInspector(string userTypeChoice)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast adgangskode: ");
            string password = Console.ReadLine();
            Console.ReadKey();

            RunInspectorMenu();



        }


        private void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("Opret ny bruger");
            Console.WriteLine();

            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast dit telefonnummer: ");
            string phone = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Du er nu oprettet som bruger og kan logge ind.");
            Console.WriteLine();
            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey();
            Start();
        }


        private void RunDriverMenu()
        {
            string title = "--- Vognmand Menu ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] driverMenuItems = { "Book vognkontrol", "Se booket tid", "Hovedmenu", "Afslut" };
            var driverMenu = new MenuDisplay(title, driverMenuItems);
            int indexChosen = driverMenu.Run();

            switch (indexChosen)
            {
                case 0:
                    //Debug_BookCarInspection();
                    BookCarInspection();
                    break;
                case 1:
                    ViewBookedTime();
                    break;
                case 2:
                    Start();
                    break;
                case 3:
                    Exit();
                    break;
                
            }
        }

        private void RunInspectorMenu()
        {
            string title = "--- Kontrollør Menu ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] inspectorMenuItems = { "Se bookede vognkontroller", "Hovedmenu", "Afslut" };
            var inspectorMenu = new MenuDisplay(title, inspectorMenuItems);
            int indexChosen = inspectorMenu.Run();

            switch (indexChosen)
            {
                case 0:
                    ViewBookedTimes();
                    break;
                case 1:
                    Start();
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        private void BookCarInspection()
        {
            
            var handler = new Datahandler();

            //Indlæser ledige tider
            var availableTimes = GetAvailableTimes();

            //Vis tider og vælg
            Console.Clear();
            Console.WriteLine("--- Ledige tider ---\n");
            ListFormatter.PrintTimeslotsAsTable(availableTimes);

            Console.Write("\nVælg en tid (nummer): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > availableTimes.Count)
            {
                Console.WriteLine("Ugyldigt valg.");
                Console.ReadKey();
                return;
            }

            DateTime selectedTime = availableTimes[choice - 1];

            //Indtast bookingoplysninger
            Console.Write("\nIndtast selskabsnavn: ");
            string company = Console.ReadLine();

            Console.Write("Indtast e-mail: ");
            string email = Console.ReadLine();
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Ugyldig e-mail.");
                Console.ReadKey();
                return;
            }

            Console.Write("Indtast telefonnummer: ");
            string phone = Console.ReadLine();

            Console.Write("Indtast vogn-ID: ");
            string carId = Console.ReadLine();

            //Tjek om vogn allerede er booket
            var bookingLines = handler.LoadBookings();

            Console.WriteLine("\n--- BOOKINGLINJER ---");
            foreach (var line in bookingLines)
            {
                Console.WriteLine($"[{line.Split(';')[1]}]"); // Vogn-ID i filen
            }
            Console.WriteLine($"\nBrugerindtastet vognID: [{carId}]");

            bool carAlreadyBooked = bookingLines
                .Any(line => line.Split(';')[1].Trim() == carId.Trim());

            /* Debugging
             * 
             *  Console.WriteLine("\n--- DEBUG VOGN-ID FRA FILEN ---");
            foreach (var line in bookingLines)
            {37
                var split = line.Split(';');
                if (split.Length > 1)
                {
                    Console.WriteLine($"ID: {split[0]}, VognID: [{split[1]}]");
                }
                else
                {
                    Console.WriteLine("⚠️ Ugyldig linje: " + line);
                }
            }
             * 
             */


            if (carAlreadyBooked)
            {
                Console.WriteLine("Denne vogn er allerede booket.");
                Console.ReadKey();
                return;
            }

            //Tildel unikt BookingID
            int nextBookingId = bookingLines
                .Select(line => int.Parse(line.Split(';')[0]))
                .DefaultIfEmpty(1000)
                .Max() + 1;

            // Gem ny booking
            string newLine = $"{nextBookingId};{carId};{company};{email};{phone};{selectedTime:yyyy-MM-dd HH:mm}";
            bookingLines.Add(newLine);

            Console.WriteLine("[DEBUG] Skriver til bookings-fil:");
            Console.WriteLine(Path.GetFullPath("Data/mock_bookings.txt"));

            handler.SaveBookings(bookingLines);

            //Bekræft
            Console.WriteLine("\nBooking gennemført!");
            Console.WriteLine("\nBookingdetaljer:");
            Console.WriteLine($"Booking ID : {nextBookingId}");
            Console.WriteLine($"Dato & Tid : {selectedTime:dd-MM-yyyy HH:mm}");
            Console.WriteLine($"Selskab    : {company}");
            Console.WriteLine($"Vogn-ID    : {carId}");
            Console.WriteLine($"E-mail     : {email}");
            Console.WriteLine($"Telefon    : {phone}");


            Console.WriteLine("\nTryk en tast for at vende tilbage.");
            Console.ReadKey();

            RunDriverMenu();



            /*Console.Clear();
            Console.WriteLine("Book vognkontrol");
            Console.WriteLine();

            // Implement code for booking a car inspection

            Console.WriteLine("Booking gennemført. Tryk på en vilkårlig tast for at vende tilbage til vognmandmenuen.");
            Console.ReadKey();

            RunDriverMenu();*/

        }

        private void ViewBookedTime()
        {
            Console.Clear();
            Console.WriteLine("Vis booket tid");
            Console.WriteLine();

            // Implement code for viewing booked time

            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til vognmandmenuen.");
            Console.ReadKey();

            RunDriverMenu();
        }

        private void ViewBookedTimes()
        {
            Console.Clear();
            Console.WriteLine("Vis bookede vognkontroller");
            Console.WriteLine();

            // implement code for viewing booked times and booking details 

            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til kontrollørmenuen.");
            Console.ReadKey();

            RunInspectorMenu();
        }

        private void CarInspectionInfo()
        {
            Console.Clear();
            Console.WriteLine("Information om vognkontrol");

            Console.WriteLine();
            Console.WriteLine("Tryk på en tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey();
            Start();
        }

        private void Exit()
        {
            Console.Clear();
            Console.WriteLine("Programmet afsluttes...");
            Thread.Sleep(2000);


        }

        List<DateTime> GetAvailableTimes()
        {
            var handler = new Datahandler();

            //alle de ledige tider fra mock_timeslots
            var allTimes = handler.LoadTimeslots()
            .Select(line => 
                    {
                        var parts = line.Split(';');
                        return DateTime.Parse(parts[0]).Add(TimeSpan.Parse(parts[1]));
                    }).ToList();

            //allerede bookede tider fra vores mock_booking
            var bookedTimes = handler.LoadBookings()
                .Select(line => DateTime.Parse(line.Split(';')[5]))
                .ToHashSet();

            //tilgængelige tider, efter de bookede er trukket fra
            return allTimes.Except(bookedTimes).ToList();
        }


        private void Debug_BookCarInspection()
        {
            Console.Clear();
            Console.WriteLine("--- DEBUG: BookCarInspection ---");

            try
            {
                Console.WriteLine("🔹 Kører GetAvailableTimes()");
                var times = GetAvailableTimes();

                Console.WriteLine($"🔹 Ledige tider fundet: {times.Count}");
                foreach (var t in times.Take(5)) // Vis kun 5 første
                {
                    Console.WriteLine($"   - {t:yyyy-MM-dd HH:mm}");
                }

                Console.WriteLine("🔹 Henter eksisterende bookinger...");
                var handler = new Datahandler();
                var bookings = handler.LoadBookings();
                Console.WriteLine($"   Antal bookinger: {bookings.Count}");

                Console.WriteLine("🔹 Forsøger at generere nyt bookingID...");
                int nextId = bookings
                    .Select(line => int.Parse(line.Split(';')[0]))
                    .DefaultIfEmpty(1000)
                    .Max() + 1;
                Console.WriteLine($"   Næste bookingID: {nextId}");

                Console.WriteLine("\n✅ Alt ser ud til at virke.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n🛑 FEJL opstod: {ex.Message}");
            }

            Console.WriteLine("\nTryk en tast for at vende tilbage.");
            Console.ReadKey();
            RunDriverMenu();
        }
    }
}
