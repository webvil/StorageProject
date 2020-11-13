using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
        
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageMaster
{
    public abstract class Vehicle
    {
        List<Product> trunk;
        public int Capacity { get; set; }
        public IReadOnlyCollection<Product> Trunk
        {
            get
            {
                return this.trunk;
            }
            set
            {
               
            }

        }
        public bool IsFull
        {
            get
            {
                double totalWeight = 0.0;
                foreach (var product in this.trunk)
                {
                    totalWeight += product.Weight;
                }
                return totalWeight >= Capacity;
            }

        }
        public bool IsEmpty
        {
            get
            {
                return this.trunk.Count == 0;
            }

        }
        public Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.trunk = new List<Product>();
        }

        public void LoadProduct(Product product)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Vehicle is full");
            }
            
            this.trunk.Add(product);
            return;

        }
        public Product UnLoad()
        {
            if (IsEmpty)
            {
                return default(Product);
            }
            
            var removedItem = trunk[trunk.Count - 1];
            trunk.RemoveAt(trunk.Count - 1);

            
            return removedItem;

        }
    }
}
