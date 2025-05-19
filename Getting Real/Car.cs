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
        
        

        public Car(int currentId, int newId)
        {
            this._currentId = currentId;
            this._newId = newId;
        }
    }
}
