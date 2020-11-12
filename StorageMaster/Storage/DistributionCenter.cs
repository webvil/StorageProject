using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class DistributionCenter : Storage
    {
        const int capacity = 2;
        const int garageSlots = 5;
        static Vehicle[] vehicles = new[] { new Van(), new Van(), new Van() };
        public DistributionCenter(string name)
            : base(name, capacity, garageSlots, vehicles) 
        {
            
        }

    }
}
