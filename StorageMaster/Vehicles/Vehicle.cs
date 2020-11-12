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
        public int Capacity { get; set; }
        public IReadOnlyCollection<Product> Trunk;


        public bool IsFull
        {
            get
            {
                double totalWeight = 0.0;
                foreach (var product in Trunk)
                {
                    totalWeight += product.Weight;
                }
                return totalWeight >= Capacity ? true : false;
            }

        }
        public bool IsEmpty
        {
            get
            {
                return Trunk.Count == 0;
            }

        }
        public Vehicle(int capacity)

        {
            this.Capacity = capacity;
            Trunk = new List<Product>();
        }

        public void LoadProduct(Product product)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Vehicle is full");
            }
            var products = new List<Product>();
            //var enumerator = Trunk.GetEnumerator();
            foreach (var item in Trunk)
            {
                products.Add(item);
            }
            products.Add(product);
            Trunk = new ReadOnlyCollection<Product>(products);
        }
        public Product UnLoad()
        {
            if (IsEmpty)
            {
                return default(Product);
            }
            var products = new List<Product>();
            foreach (var item in Trunk)
            {
                products.Add(item);
            }
            var removeItem = products[products.Count - 1];
            products.RemoveAt(products.Count - 1);

            // var enumerator = Trunk.GetEnumerator();

            Trunk = new ReadOnlyCollection<Product>(products);
            return removeItem;

        }
    }
}
