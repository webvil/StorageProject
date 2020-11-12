using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class AutomatedWarehouse : Storage
    {
        const int capacity = 1;
            const int garageSlots = 2;
            static Vehicle[] vehicles = new[] { new Truck() };
        public AutomatedWarehouse(string name) 
            :base(name, capacity, garageSlots, vehicles: vehicles){}
        
    }
}
