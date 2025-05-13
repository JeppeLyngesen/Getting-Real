using System;

namespace Getting_Real
{
    internal class Program
     
    {
        private static ASCII logo = new ASCII();
        static void Main(string[] args)
        {

           

            var asciiLogo = new ASCII();
            asciiLogo.ShowSydtrafikLogo();
            ManagementSystem Sydtrafik = new ManagementSystem();
            Sydtrafik.Start();



        }

        static void ShowLogo()
        {
            Console.Clear();

            logo.ShowSydtrafikLogo();
        }

    }
}
