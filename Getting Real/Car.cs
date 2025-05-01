using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    internal class Car
    {
        private int _currentId { get; set; }
        private int _newId { get; set; }
        public enum CarType
        {
            Sedan = 0,
            StationCar = 1,
            SUV = 2,
            Minivan = 3
        }
        public CarType Type { get; set; }

        public Car(int currentId, int newId, CarType type)
        {
            this._currentId = currentId;
            this._newId = newId;
            this.Type = type;
        }

        public override string ToString()
        {
            return $"{_currentId};{_newId};{Type}";
        }

        public static Car FromString(string input)
        {
            var parts = input.Split(';');

            if (parts.Length != 3)
            {
                throw new FormatException("Input needs exactly two semicolon seperators");
            }

            if (!int.TryParse(parts[0], out int currentId))
            {
                throw new FormatException("currentId is not an integer");
            }

            if (!int.TryParse(parts[1], out int newId))
            {
                throw new FormatException("newId is not an integer");
            }

            if (!Enum.TryParse(parts[2], out CarType type))
            {
                throw new FormatException("CarType is not a vaild CarType");
            }

            return new Car(currentId, newId, type);
        }
    }
}
