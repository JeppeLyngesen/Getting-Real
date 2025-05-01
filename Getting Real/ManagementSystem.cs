using System;
using System.Collections.Generic;

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
            Console.Clear();
            Console.WriteLine("Book vognkontrol");
            Console.WriteLine();

            // Implement code for booking a car inspection

            Console.WriteLine("Booking gennemført. Tryk på en vilkårlig tast for at vende tilbage til vognmandmenuen.");
            Console.ReadKey();

            RunDriverMenu();

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
    }
}
