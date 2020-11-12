using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    
    public abstract class Product
    {
        public Product(double price, double weight)
        {
            Price = price;
            Weight = weight;
        }
        
        private double price;

        public double Price
        {
            get { return price; }
            set {
                if (value < 0)
                {
                    throw new InvalidOperationException("Price cannot be negative");
                }
                price = value; }
        }
        public double Weight { get; set; }

    }
}
