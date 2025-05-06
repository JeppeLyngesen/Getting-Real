using System;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class ASCII
    {

        public void ShowSydtrafikLogo()
        {


            Console.BackgroundColor = ConsoleColor.DarkBlue;


            string[] asciiLogo = new string[]
            {
        @"                                                                               ",
        @"                                                                               ",
        @"    ____ __     ________  _______________      ___    _______ __  ___   ___    ",
        @"   / ____|\ \  / /|  __  \        |       \   /   \  |  _____|  | |  | /  /    ",
        @"  | (___   \ \/ / | |  | |--|  |---|  |\  /  /  /\ \ |  |_   |  | |  |/  /     ",
        @"   \___ \\  \  /  | |  | |  |  |   |     /  /  __  \ |  __|  |  | |     /      ",
        @"   ____) |   | |  | |__| /  |  |   |  |\ \ /  /  \  \|  |    |  | |  |\  \     ",
        @"  |_____/    |_|  |_____/   |__|   |__| \_\__/    \__\ _|    |__| |__| \__\    ",
        @"                                                                               ",
        @"                             – vi kører for dig                                ",
        @"                                                                               "
            };


            foreach (var line in asciiLogo)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(line.Substring(1, line.Length - 2));
                Console.WriteLine(line.Substring(line.Length - 1));
            }


            Console.ResetColor();
            Console.WriteLine();
        }

    }
}

