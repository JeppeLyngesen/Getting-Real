using System;

namespace Getting_Real
{
    public class Program
     
    {
        
        static void Main(string[] args)
        {

            UserManager userManager = new UserManager();

            ASCII.ShowSydtrafikLogo();

            
            ManagementSystem Sydtrafik = new ManagementSystem();

            Console.WriteLine("--- Velkommen til Sydtrafiks bookingsystem for vognkontrol ---");
            Console.WriteLine("\nTryk på en vilkårlig tast for at fortsætte...");
            Console.ReadKey(true);

            Sydtrafik.Start();



        }

      

    }
}
