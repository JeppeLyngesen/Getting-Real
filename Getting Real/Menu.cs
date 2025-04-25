using System;

namespace Getting_Real
{
    public class MenuVisning
    {
        private int _indexValgt;
        private string[] _menuPunkter;
        private string _overskrift;

        public MenuVisning(string overskrift, string[] menuPunkter)
        {
            _indexValgt = 0;
            _menuPunkter = menuPunkter;
            _overskrift = overskrift;
        }

        public int Kør()
        {
            ConsoleKey tastInput;
            do
            {
                Console.Clear();
                VisMenuPunkter();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                tastInput = keyInfo.Key;

                if (tastInput == ConsoleKey.UpArrow)
                {
                    _indexValgt--;
                    if (_indexValgt == -1)
                    {
                        _indexValgt = _menuPunkter.Length - 1;
                    }
                }
                else if (tastInput == ConsoleKey.DownArrow)
                {
                    _indexValgt++;
                    if (_indexValgt == _menuPunkter.Length)
                    {
                        _indexValgt = 0;
                    }
                }
            } while (tastInput != ConsoleKey.Enter);

            return _indexValgt;
        }

        private void VisMenuPunkter()
        {
            Console.WriteLine(_overskrift);
            for (int i = 0; i < _menuPunkter.Length; i++)
            {
                string valgtMenuPunkt = _menuPunkter[i];
                string prefix;

                if (i == _indexValgt)
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
                Console.WriteLine($"{prefix} << {valgtMenuPunkt} >>");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
