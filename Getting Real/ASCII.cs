using System;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class ASCII
    {

        public static void ShowSydtrafikLogo()
        {
            
                       
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            

            string[] asciiLogo = new string[]
            {
        @"                                                                               ",
        @"                                                                               ",
        @"    _____ __    ________  _______________      ___    _______ __  ___   ___    ",
        @"   / ____|\ \  / /|  __  \        |       \   /   \  |  _____|  | |  | /  /    ",
        @"  | (___   \ \/ / | |  | |--|  |---|  |\  /  /  /\ \ |  |_   |  | |  |/  /     ",
        @"   \___ \   \  /  | |  | |  |  |   |     /  /  __  \ |  __|  |  | |     /      ",
        @"   ____) |   | |  | |__| /  |  |   |  |\ \ /  /  \  \|  |    |  | |  |\  \     ",
        @"  |_____/    |_|  |_____/   |__|   |__| \_\__/    \__\ _|    |__| |__| \__\    ",
        @"                                                                               ",
        @"                             – vi kører for dig                                ",
        @"                                                                               "
            };

            
            foreach (string line in asciiLogo)
            {
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(line);

            }


            Console.ResetColor(); 
            Console.WriteLine(); 
        }

    }
}

