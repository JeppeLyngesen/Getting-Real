using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    class Login
    {
        public static void Main()
        {
            // Angiv brugernavn der skal bruges
            string correctUsername = "admin";

            // Sti til adgangskodefilen
            string folderPath = @"C:\SecurePasswords";
            string filePath = Path.Combine(folderPath, "password.txt");

            // Tjek om adgangskodefilen findes
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Adgangskodefilen blev ikke fundet!");
                return;
            }

            // Læs adgangskoden fra filen
            string correctPassword = File.ReadAllText(filePath);

            // Bed brugeren om at indtaste brugernavn
            Console.Write("Brugernavn: ");
            string inputUsername = Console.ReadLine();

            // Bed brugeren om at indtaste adgangskode
            Console.Write("Adgangskode: ");
            string inputPassword = ReadPassword();

            // Tjek om både brugernavn og adgangskode er korrekte

            if (inputUsername == correctUsername && inputPassword == correctPassword)
            {
                Console.WriteLine("Login lykkedes! Velkommen til Sydtrafik, admin.");

                // Vis menu hvis login lykkes
                string[] menuItems = { "Vis bookinger", "Opret booking", "Afslut" };
                MenuDisplay menu = new MenuDisplay("Hovedmenu", menuItems);
                int valgt = menu.Run();

                // Du kan reagere på brugerens valg her
                Console.WriteLine($"Du valgte: {menuItems[valgt]}");
            }

            else
            {
                Console.WriteLine("Forkert brugernavn eller adgangskode.");
            }

            Console.WriteLine("Tryk på en tast for at afslutte...");
            Console.ReadKey();


        }



        // Metode til at læse adgangskoden uden at vise den på skærmen
        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                // Hvis brugeren trykker på backspace, fjern et tegn
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Fjern det sidste tegn fra konsollen
                }
                else if (!char.IsControl(key.KeyChar)) // Hvis det ikke er en kontroltast (som Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Erstat indtastningen med et stjernesymbol
                }
            }
            Console.WriteLine(); // Gå til næste linje efter passwordinput
            return password;


        }
    }



    
}
