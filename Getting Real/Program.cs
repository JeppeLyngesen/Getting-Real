using System;

namespace Getting_Real
{
    internal class Program
     
    {
        private static ASCII logo = new ASCII();
        static void Main(string[] args)
        {

            Thread logoThread = new Thread(ShowLogo);
            logoThread.Start();


            Thread.Sleep(3000);


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
