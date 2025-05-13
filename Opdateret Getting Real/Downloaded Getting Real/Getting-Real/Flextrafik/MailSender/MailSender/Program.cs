using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // Informer brugeren om, at SMTP-klienten starter
            Console.WriteLine("Starter opsætning af SMTP-klient...");

            // Opretter SMTP-klient og angiver server, port og loginoplysninger
            SmtpClient smtpClient = new SmtpClient("192.168.242.135", 25)
            {
                EnableSsl = false, // SSL er slået fra
                UseDefaultCredentials = false, // Brug ikke Windows-konto
                Credentials = new NetworkCredential("administrator", "Password1234"), // SMTP-login
                DeliveryMethod = SmtpDeliveryMethod.Network // Send direkte via netværk
            };

            Console.WriteLine("SMTP-klient sat op.");

            // Definer brugernavn
            string username = "admin";

            // Generér en adgangskode med 12 tegn
            string generatedPassword = GeneratePassword(12);

            // Definer sti til sikker mappe og fil
            string folderPath = @"C:\SecurePasswords";
            string filePath = Path.Combine(folderPath, "password.txt");

            // Opret mappen hvis den ikke findes
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Mappen 'SecurePasswords' blev oprettet.");
            }

            // Gem adgangskoden i den sikre mappe
            File.WriteAllText(filePath, generatedPassword);


            // Opretter en e-mail
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("administrator@bauer.local"), // Afsender
                Subject = "Dit midlertidige login til systemet", // Emne
                Body = $"Hej {username},\n\nHer er din midlertidige adgangskode:\n\n" +
                       $"Brugernavn: {username}\nAdgangskode: {generatedPassword}\n\n" +
                       $"Husk at ændre den ved første login.\n\nVenlig hilsen,\nSystemadministrator"
            };

            // Tilføj modtager
            mail.To.Add("admin@bauer.local");

            // Informer om at e-mail er klar til at blive sendt
            Console.WriteLine("Mail oprettet. Klar til afsendelse...");

            // Send e-mail
            smtpClient.Send(mail);

            // Bekræft at e-mail og adgangskode er sendt/gemt
            Console.WriteLine("E-mail sendt og adgangskode gemt i 'password.txt'.");
        }
        catch (Exception ex)
        {
            // Hvis der opstår fejl, vis den
            Console.WriteLine("Fejl ved afsendelse:");
            Console.WriteLine(ex.ToString());
        }

        // Vent på tast før programmet lukker
        Console.ReadKey();
    }

    // Metode til at generere en stærk adgangskode
    static string GeneratePassword(int length)
    {
        // Tegn der kan indgå i adgangskoden
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%&*?";

        // Bruges til at bygge adgangskoden
        StringBuilder sb = new StringBuilder();

        // Brug en kryptografisk sikker random-generator
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] buffer = new byte[sizeof(uint)];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(buffer); // Fyld buffer med tilfældige bytes
                uint num = BitConverter.ToUInt32(buffer, 0); // Konverter til heltal
                sb.Append(validChars[(int)(num % (uint)validChars.Length)]); // Vælg tegn
            }
        }

        return sb.ToString(); // Returnér adgangskoden
    }
}

