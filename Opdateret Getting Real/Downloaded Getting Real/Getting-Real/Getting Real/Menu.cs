using System;

namespace Getting_Real
{
    public class MenuDisplay
    {
        private int _indexChosen;
        private string[] _menuItems;
        private string _title;

        public MenuDisplay(string title, string[] menuItems)
        {
            _indexChosen = 0;
            _menuItems = menuItems;
            _title = title;
        }

        public int Run()
        {
            ConsoleKey userInput;
            do
            {
                Console.Clear();
                ShowMenuItems();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                userInput = keyInfo.Key;

                if (userInput == ConsoleKey.UpArrow)
                {
                    _indexChosen--;
                    if (_indexChosen == -1)
                    {
                        _indexChosen = _menuItems.Length - 1;
                    }
                }
                else if (userInput == ConsoleKey.DownArrow)
                {
                    _indexChosen++;
                    if (_indexChosen == _menuItems.Length)
                    {
                        _indexChosen = 0;
                    }
                }
            } while (userInput != ConsoleKey.Enter);

            return _indexChosen;
        }

        private void ShowMenuItems()
        {
            Console.WriteLine(_title);
            for (int i = 0; i < _menuItems.Length; i++)
            {
                string ChosenMenuItem = _menuItems[i];
                string prefix;

                if (i == _indexChosen)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{prefix} << {ChosenMenuItem} >>");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
