using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    class Warehouse : Storage
    {
        const int capacity = 10;
        const int garageSlots = 10;
        static Vehicle[] vehicles = new Vehicle[] { new Semi(), new Semi(), new Semi() };
        
        public Warehouse(string name)
            : base(name, capacity, garageSlots, vehicles) 
        {
            
        }
    }
}
