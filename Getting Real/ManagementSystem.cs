using System;
using System.Collections.Generic;


namespace Getting_Real
{
    public class ManagementSystem
    {
        private UserManager userManager = new UserManager();
        private static Dictionary<string, string> inspectors = new Dictionary<string, string>
        {
            { "bruger1", "1234" },
            { "kontrollør2", "password2" } // (Ikke sikkert at have her)
        };

        private string _loggedInEmail;
        private string _loggedInUser;

        public void Start()
        {
            userManager.LoadUsersFromFile("users.csv");

            

            // Main Menu
            string title = "--- Booking af vognkontrol hos Sydtrafik ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] MainMenuItems = { "Bookingmenu", "Info om vognkontrol", "Brugeradministration", "Afslut" };
            MenuDisplay mainMenu = new MenuDisplay(title, MainMenuItems);
            int indexChosen = mainMenu.Run();

            switch (indexChosen)
            {
                case 0:
                    BookingMenu();
                    break;
                case 1:
                    CarInspectionInfo();
                    break;
                case 2:
                    UserManagement();
                    break;
                case 3:                    
                    Exit();
                    return;
            }

        }

        private void BookingMenu()
        {

            string title = "--- Vælg din brugertype ---\n\nVælg ønsket brugertype og tast enter.\n";
            string[] userTypeMenuItems = { "Selskab", "Kontrollør", "Afslut" };
            MenuDisplay userTypeMenu = new MenuDisplay(title, userTypeMenuItems);
            int userTypeChoice = userTypeMenu.Run();

            switch (userTypeChoice)
            {
                case 0:
                    LoginCompany("selskab");
                    break;
                case 1:
                    LoginInspector("kontrollør");
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        private void LoginCompany(string userTypeChoice)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();
            _loggedInEmail = email; // Gemmer email til senere brug i ViewBookedTimeCompany

            Console.Write("Indtast adgangskode: ");
            string password = Console.ReadLine();

            User user = userManager.GetUserByEmailAndPassword(email, password); // Finder bruger i listen/ systemet


            if (user != null)
            {
                Console.WriteLine("Login succesfuldt!");
                Console.ReadKey();
                RunDriverMenu();
            }
            else
            {
                Console.WriteLine("Login mislykkedes. Forkert e-mail eller adgangskode.");
                Console.ReadKey();
                Start();
            }


        }

        private void LoginInspector(string userTypeChoice)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast dit brugernavn: ");
            string userName = Console.ReadLine();
            _loggedInUser = userName; // Gemmer brugernavn til senere brug

            Console.Write("Indtast adgangskode: ");
            string password = Console.ReadLine();
            if (InspectorsLogin(userName, password))
            {
                _loggedInUser = userName;
                Console.WriteLine($"Velkommen, {_loggedInUser}!");

                Console.ReadKey();
                RunInspectorMenu(); 
            }
            else
            {
                Console.WriteLine("Login mislykkedes. Forkert brugernavn eller adgangskode.");
                Console.ReadKey();
                Start(); 
            }
        }

        
        private bool InspectorsLogin(string email, string password) // Tjekker om brugernavnet og adgangskoden matcher i opslagstabellen
        {
            if (inspectors.ContainsKey(email))
            {
                return inspectors[email] == password;
            }
            return false; // Returner false, hvis e-mailen ikke findes i opslagstabellen
                       

        }


        private void UserManagement()
        {

            string title = "--- Brugeradministration ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] UserManagementItems = { "Opret bruger", "Glemt adgangskode", "Slet bruger", "Hovedmenu", "Afslut" };
            MenuDisplay userMenu = new MenuDisplay(title, UserManagementItems);
            int indexChosen = userMenu.Run();

            switch (indexChosen)
            {
                case 0:
                    RegisterUser();
                    break;
                case 1:
                    ForgottenPassword();
                    break;
                case 2:
                    DeleteUser();
                    break;
                case 3:
                    Start();
                    break;
                case 4:
                    Exit();
                    return;
            }
        }


        private void RegisterUser()
        {

            Console.Clear();
            Console.WriteLine("--- Opret bruger ---");
            Console.WriteLine();
            userManager.RegisterUser();

            userManager.SaveUsersToFile("users.csv");
            Console.WriteLine("\nBruger oprettet. Tryk på en tast for at komme til bookingmenuen.");
            Console.ReadKey();
            Start();
        }

        private void ForgottenPassword()
        {
            Console.Clear();
            Console.WriteLine("--- Glemt adgangskode ---\n");
            userManager.ResendPassword();
            Console.WriteLine("\nTryk på en tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey();
            Start();
        }

        private void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("--- Slet bruger ---\n");
            userManager.DeleteUser();
            userManager.SaveUsersToFile("users.csv");
            Console.WriteLine("\nTryk på en tast for at vende tilbage til hovedmenuen.");
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
                    ViewBookedTimeCompany();
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
            DateTime? selectedTime = ListFormatter.PromptUserToSelectTimeslot(availableTimes);

            if (selectedTime == null)
            {
                Console.WriteLine("Booking annulleret.");
                Console.ReadKey();
                RunDriverMenu();
            }

            DateTime selectedTimeValue = selectedTime.Value;

            //Indtast bookingoplysninger
            Console.Write("\nIndtast selskabsnavn: ");
            string company = Console.ReadLine();

            Console.Write("Indtast e-mail: ");
            string email = Console.ReadLine();
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Ugyldig e-mail.");
                Console.ReadKey();
                RunDriverMenu();
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
                    Console.WriteLine("Ugyldig linje: " + line);
                }
            }
             * 
             */


            if (carAlreadyBooked)
            {
                Console.WriteLine("Denne vogn er allerede booket.");
                Console.ReadKey();
                RunDriverMenu();
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

        private void ViewBookedTimeCompany() //Chauffør side
        {
            Console.Clear();
            Console.WriteLine("--- Dine bookinger ---\n");

            var handler = new Datahandler();
            var allBookings = handler.LoadBookings();

            var userBookings = allBookings
                .Where(line => line.Split(';')[3].Trim().Equals(_loggedInEmail, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!userBookings.Any())
            {
                Console.WriteLine("Du har ingen aktive bookinger.");
                Console.WriteLine("Tryk på en vilkårlig tast for at gå tilbage.");
                Console.ReadKey();
                RunDriverMenu();
                return;
            }

            ListFormatter.PrintBookingsWithCarID(userBookings);

            Console.WriteLine("\nIndtast VognID på booking du vil slette, eller tryk [Enter] for at gå tilbage:");
            
            string carIdInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(carIdInput))
            {
                RunDriverMenu();
                return;
            }

            var bookingToRemove = userBookings.FirstOrDefault(b => b.Split(';')[1].Trim().Equals(carIdInput.Trim(), StringComparison.OrdinalIgnoreCase));

            if (bookingToRemove != null)
            {
                allBookings.Remove(bookingToRemove);
                handler.SaveBookings(allBookings);
                Console.WriteLine($"\nBooking med VognID '{carIdInput}' er slettet.");
            }
            else
            {
                Console.WriteLine($"\nIngen booking fundet med VognID '{carIdInput}'.");
            }

            Console.WriteLine("\nTryk på en vilkårlig tast for at gå tilbage.");
            Console.ReadKey();
            RunDriverMenu();
        }

        private void ViewBookedTimes() //Kontrollør side
        {
            Console.Clear();
            Console.WriteLine("Vis bookede vognkontroller");
            var handler = new Datahandler();
            var allBookings = handler.LoadBookings();

            allBookings.ToList();
            ListFormatter.PrintBookingsWithCompanyName(allBookings);

            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til kontrollørmenuen.");
            Console.ReadKey();

            RunInspectorMenu();
        }

        private void CarInspectionInfo() 
        {
            Console.Clear();
            Console.WriteLine("--- Information om vognkontrol ---");
            Console.WriteLine();
            Console.WriteLine("Vognkontrollen skal sikre, at alle køretøjer er i god stand og opfylder sikkerhedskravene.");
            Console.WriteLine();
            Console.WriteLine("Kontrollen omfatter en grundig inspektion af køretøjets mekaniske og elektriske systemer, bremser, dæk, lys og sikkerhedsudstyr.");
            Console.WriteLine();
            Console.WriteLine("Vognkontrollen udføres af autoriserede kontrollører.");
            Console.WriteLine();
            Console.WriteLine("For at booke en vognkontrol, skal du vælge en ledig tid og indtaste de nødvendige oplysninger.");          
            Console.WriteLine();
            Console.WriteLine("Hvis du har spørgsmål eller brug for hjælp, kan du kontakte Sydtrafik.");


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

        public List<DateTime> GetCompanyBookedTimes(string company)  //Henter bookinger baseret på selskab
        {
            var handler = new Datahandler();
            var companyBookings = handler.LoadBookings()
                .Where(line => line.Split(';')[2].Trim().Equals(company, StringComparison.OrdinalIgnoreCase))
                .Select(line => DateTime.Parse(line.Split(';')[5]))
                .ToList();

            return companyBookings;
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
