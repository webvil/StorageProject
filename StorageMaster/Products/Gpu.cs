using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class Gpu : Product
    {
        public Gpu(double price) : base(price, 0.7)
        {
            
        }
        //public const double GpuWeight = 0.7;

       
    }
}
