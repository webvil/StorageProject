using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class Van : Vehicle
    {
        
        const int VAN_CAPACITY = 2;
        public Van() : base(VAN_CAPACITY) { }
    }
}
