using Getting_Real;
using System;
using System.IO;

namespace Getting_Real
{
    public class MainStarter
    {
        static void Main()
        {
            // Starter mailsender  
            Program1.Main();

            // Starter login og menu  
            Login.Main();


            // Starter ASCII logo  
            ASCII ascii = new ASCII();
            ascii.ShowSydtrafikLogo();
            Console.ReadKey();

            // Kalder ManagementSystem Start metoden
            ManagementSystem managementSystem = new ManagementSystem();
            managementSystem.Start();

            
        }
    }

}

            
            

