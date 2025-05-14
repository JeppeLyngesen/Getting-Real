using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    internal class UserManager
    {
        private List<User> users = new List<User>();

        public UserManager() 
        { 

            LoadUsersFromFile("users.csv"); 

        }

        public void RegisterUser()
        {

            Console.Write("Indtast navn: ");
            string name = Console.ReadLine();

            Console.Write("Indtast e-mail: ");
            string email = Console.ReadLine();
            Console.WriteLine();

            
            if (users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase))) 
            {
                Console.WriteLine("En bruger med denne e-mail findes allerede.");
                return;
            }

            string password = GeneratePassword();
            var user = new User { Name = name, Email = email, Password = password};

            users.Add(user);

            SendLoginEmail(user);
            Console.WriteLine($"Bruger oprettet. Adgangskode sendt til: {email}\n");

            SaveUsersToFile("users.csv");
        }


        public void SaveUsersToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.Name};{user.Email};{user.Password}");
                }
            }
        }

        public void LoadUsersFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 3)
                        {
                            var user = new User { Name = parts[0], Email = parts[1], Password = parts[2] };
                            users.Add(user);
                        }
                    }
                }
            }
        }

        public User Login()
        {
            Console.Write("Indtast din e-mail: ");
            string loginEmail = Console.ReadLine();

            Console.Write("Indtast din adgangskode: ");
            string loginPassword = Console.ReadLine();

            return users.Find(u => u.Email == loginEmail && u.Password == loginPassword);
        }

        public bool Login(string email, string password)
        {
            return users.Exists(u => u.Email == email && u.Password == password);
        }

        private string GeneratePassword()
        {
            var rand = new Random();
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            char[] password = new char[6];
            for (int i = 0; i < password.Length; i++)
            {
                password[i] = chars[rand.Next(chars.Length)];
            }
            return new string(password);
        }

        private void SendLoginEmail(User user)
        {
            string fromEmail = "gettingrealprojekt@gmail.com";
            string fromPassword = "fhum maag tztc lsmo";   // app-password fra Gmail (ikke sikkert at have her)

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            };

            var message = new MailMessage(fromEmail, user.Email)
            {
                Subject = "Din adgangskode",
                Body = $"Hej {user.Name},\n\nHer er din pensonlige adgangskode: {user.Password}\n\nHilsen\nSydtrafik Loginsystem"
            };

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved afsendelse: " + ex.Message);
            }
        }


        public void ResendPassword()
        {
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            var user = users.Find(u => u.Email == email);
            if (user != null)
            {
                SendLoginEmail(user);
                Console.WriteLine("Adgangskode er gensendt til din e-mail.");
            }
            else
            {
                Console.WriteLine("Bruger ikke fundet.");
            }
            Thread.Sleep(2000);
        }

        public void DeleteUser()
        {
            Console.Write("Indtast din e-mail: ");
            string email = Console.ReadLine();

            Console.Write("Indtast din adgangskode: ");
            string password = Console.ReadLine();

            var user = users.Find(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                Console.WriteLine("Er du sikker på, at du vil slette din bruger? (ja/nej)");
                string confirm = Console.ReadLine();
                if (confirm.ToLower() == "ja")
                {
                    users.Remove(user);
                    SendDeletionEmail(user);
                    Console.WriteLine("Bruger er nu slettet, og bekræftelsesmail sendt.");
                }
                else
                {
                    Console.WriteLine("Bruger blev ikke slettet.");
                }
            }
            else
            {
                Console.WriteLine("Forkert e-mail eller adgangskode.");
            }
        }
        private void SendDeletionEmail(User user)
        {
            string fromEmail = "gettingrealprojekt@gmail.com";
            string fromPassword = "fhum maag tztc lsmo"; // app-kode fra Gmail (ikke sikkert at have her)

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            };

            var message = new MailMessage(fromEmail, user.Email)
            {
                Subject = "Bekræftelse på sletning af bruger",
                Body = $"Hej {user.Name},\n\nDin brugerprofil er nu blevet slettet fra Sydtrafik Loginsystem.\n\nVenlig hilsen\nSydtrafik Support"
            };

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved afsendelse af bekræftelsesmail: " + ex.Message);
            }
        }
        public User GetUserByEmailAndPassword(string email, string password)
        {
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }




    }
}

