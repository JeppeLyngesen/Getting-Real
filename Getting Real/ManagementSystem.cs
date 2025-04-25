using System;
using System.Collections.Generic;

namespace Getting_Real
{
    public class ManagementSystem
    {

        public void Start()
        {

            // Hovedmenu
            string overskrift = "--- Booking af vognkontrol hos Sydtrafik ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] hovedMenuPunkter = { "Bookingmenu", "Info om vognkontrol", "Opret bruger", "Afslut" };
            MenuVisning hovedMenu = new MenuVisning(overskrift, hovedMenuPunkter);
            int indexValgt = hovedMenu.Kør();

            switch (indexValgt)
            {
                case 0: // Bookingmenu - book tid, se/ ændre/ annullere booking
                    VælgBrugerType();
                    break;
                case 1: // Info om vognkontrol
                    InfoOmVognkontrol();
                    break;
                case 2: // Opret bruger
                    OpretBruger(); // Funktion til at oprette en ny bruger med login
                    break;
                case 3: // Afslut
                    Afslut();
                    return;
            }

        }

        private void VælgBrugerType()
        {

            string overskrift = "--- Vælg din brugertype ---\n\nVælg ønsket brugertype og tast enter.\n";
            string[] brugertypeMenupunkter = { "Vognmand", "Kontrollør", "Afslut" };
            MenuVisning brugertypeMenu = new MenuVisning(overskrift, brugertypeMenupunkter);
            int rolletypeValg = brugertypeMenu.Kør();

            switch (rolletypeValg)
            {
                case 0:
                    LoginVognmand("vognmand");
                    break;
                case 1:
                    LoginKontrollør("kontrollør");
                    break;
                case 2:
                    Afslut();
                    break;
            }
        }

        private void LoginVognmand(string rolletypeValg)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast adgangskode: ");
            string adgangskode = Console.ReadLine();
            Console.ReadKey();

            KørVognmandMenu();



        }

        private void LoginKontrollør(string rolletypeValg)
        {
            Console.Clear();
            Console.WriteLine($"--- Login ---");
            Console.WriteLine();
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast adgangskode: ");
            string adgangskode = Console.ReadLine();
            Console.ReadKey();

            KørKontrollørMenu();



        }


        private void OpretBruger()
        {
            Console.Clear();
            Console.WriteLine("Opret ny bruger");
            Console.WriteLine();

            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast dit telefonnummer: ");
            string telefon = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Du er nu oprettet som bruger og kan logge ind.");
            Console.WriteLine();
            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey();
            Start();
        }


        private void KørVognmandMenu()
        {
            string overskrift = "--- Vognmand Menu ---\n\nVælg ønsket menupunkt og tast enter.\n";
            string[] vognmandMenuPunkter = { "Book vognkontrol", "Se booket tid", "Hovedmenu", "Afslut" };
            var vognmandMenu = new MenuVisning(overskrift, vognmandMenuPunkter);
            int indexValgt = vognmandMenu.Kør();

            switch (indexValgt)
            {
                case 0:
                    BookVognkontrol();
                    break;
                case 1:
                    SeBooketTid();
                    break;
                case 2:
                    Start();
                    break;
                case 3:
                    Afslut();
                    break;
            }
        }

        private void KørKontrollørMenu()
        {
            string overskrift = "--- Kontrollør Menu ---\nVælg ønsket menupunkt og tast enter.\n";
            string[] kontrollørMenuPunkter = { "Se bookede vognkontroller", "Hovedmenu", "Afslut" };
            var kontrollørMenu = new MenuVisning(overskrift, kontrollørMenuPunkter);
            int indexValgt = kontrollørMenu.Kør();

            switch (indexValgt)
            {
                case 0:
                    SeBookedeKontroller();
                    break;
                case 1:
                    Start();
                    break;
                case 2:
                    Afslut();
                    break;
            }
        }

        private void BookVognkontrol()
        {
            Console.Clear();
            Console.WriteLine("Book vognkontrol");
            Console.WriteLine();

            // implementere kode for booking af vognkontrol

            Console.WriteLine("Booking gennemført. Tryk på en vilkårlig tast for at vende tilbage til vognmandmenuen.");
            Console.ReadKey();

            KørVognmandMenu();

        }

        private void SeBooketTid()
        {
            Console.Clear();
            Console.WriteLine("Vis booket tid");
            Console.WriteLine();

            // Implementere kode for at vise booket tid (vise, ændre, annullere)

            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til vognmandmenuen.");
            Console.ReadKey();

            KørVognmandMenu();
        }

        private void SeBookedeKontroller()
        {
            Console.Clear();
            Console.WriteLine("Vis bookede vognkontroller");
            Console.WriteLine();

            // Implementere kode for at vise bookede vognkontroller og detaljer om bookingerne

            Console.WriteLine("Tryk på en vilkårlig tast for at vende tilbage til kontrollørmenuen.");
            Console.ReadKey();

            KørKontrollørMenu();
        }

        private void InfoOmVognkontrol()
        {
            Console.Clear();
            Console.WriteLine("Information om vognkontrol");

            Console.WriteLine();
            Console.WriteLine("Tryk på en tast for at vende tilbage til hovedmenuen.");
            Console.ReadKey();
            Start();
        }

        private void Afslut()
        {
            Console.Clear();
            Console.WriteLine("Programmet afsluttes...");
            Thread.Sleep(2000);


        }
    }
}
