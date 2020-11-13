using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StorageMaster
{
    public class StoregeMaster 
    {
        public IDictionary<string, Storage> storageRegistry = new Dictionary<string, Storage>();
        List<Product> productPool = new List<Product>();
        Vehicle currentVehicle;
        public string AddProduct(string type, double price)
        {

            switch (type)
            {
                case "Gpu":
                    productPool.Add(new Gpu(price));     
                    break;
                case "SolidStateDrive":
                    productPool.Add(new SolidStateDrive(price));
                    break;
                case "HardDrive":
                    productPool.Add(new HardDrive(price));
                    break;
                case "Ram":
                    productPool.Add(new Ram(price));
                    break;
                default:
                    throw new InvalidOperationException("Invalid product type");
            }

            
            

                
            
            
              return $"Added {type} to pool.";
                    
        }
           
        
        public string RegisterStorage(string type, string name)
        {
            if (storageRegistry.ContainsKey(name))
            {

                //return default(Storage);
                return "A store with the same name already exists";
            }
            if (type == "AutomatedWarehouse")
            {
                storageRegistry.Add(name, new AutomatedWarehouse(name));
            }
            else if (type == "DistributionCenter")
            {
                storageRegistry.Add(name, new DistributionCenter(name));
            }
            else if (type == "Warehouse")
            {
                storageRegistry.Add(name, new Warehouse(name));
            }
            else
            {
                throw new InvalidOperationException("Invalid storage type!");
            }

            //return storageRegistry.ToList()[0].Value;
            return $"Registered {name}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageName"></param>
        /// <param name="garageSlot"></param>
        /// <returns></returns>
        public string SelectVehicle(string storageName, int garageSlot)
        {

            var storage = storageRegistry[storageName];
            currentVehicle = storage.GetVehicle(garageSlot);
            return $"Selected { currentVehicle.GetType().Name }";
            
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            
            int count = 0;
            foreach (var name in productNames)
            {
                Console.WriteLine("Trying to load " + name);
                foreach (var p in productPool)
                {
                    Console.WriteLine(p.GetType().Name + " in stock");                    
                    if (p.GetType().Name == name)
                    {
                        currentVehicle.LoadProduct(p);
                        count++;
                        productPool.Remove(p);
                        break;
                    }
                   
                    
                    /*else
                    {
                        throw new InvalidOperationException(name + " is out of stock!");

                    }*/

                }

            }
            return $"Loaded { count}/{ productNames.Count()} products into { currentVehicle.GetType().Name }";
            
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            SelectVehicle(sourceName, sourceGarageSlot);
            var destinationStorage = storageRegistry[destinationName];
            var sourceStorage = storageRegistry[sourceName];
           
            //for (int i = 0; i < destinationStorage.Garage.Count; i++)
            //{
            //if (destinationStorage.Garage.ToList()[i] == null)
            //{
            //Console.WriteLine("Slot " +i+ " is empty");

            //openSlot = i;
            var openSlot = destinationStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            
                //}

           // }
            
            if (sourceStorage == null) throw new InvalidOperationException("Invalid source storage!");
            if (destinationStorage == null) throw new InvalidOperationException("Invalid destination storage!");
            return $"Sent {destinationStorage.GetType().Name} to {destinationName} (slot {openSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var storage = storageRegistry[storageName];
            /*foreach (var vehicle in storage.Garage.ToList())
            {
                Console.WriteLine(vehicle);
            }*/
            
            var productsInVehicle = storage.GetVehicle(garageSlot).Trunk.Count;
            //int unloadedProductsCount = ;
            return $"Unloaded { unloadedProductsCount}/{ productsInVehicle} products at { storageName}";
            
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = storageRegistry[storageName];
            var productsCount = storage.Products.Count;

            var StockInfo = storage.Products
                                 .GroupBy(f => f.GetType().Name)
                                 .OrderByDescending(g => g.Count())
                                 .ThenBy(h => h.Key);

            List<string> GarageVehicleNames = new List<string>();
            foreach (var vehicle in storage.Garage)
            {
                if (vehicle == null)
                    GarageVehicleNames.Add("empty");
                else
                    GarageVehicleNames.Add(vehicle.GetType().Name);
            }

            double productsWeights = storage.Products.Sum(p => p.Weight);
            int storageCapacity = storage.Capacity;
            string StockFormat = "{ 0 }/{ 1 }";

            return $"Stock ({productsWeights} / {storageCapacity}) : [{StockInfo} \nGarage: [{GarageVehicleNames}]";

        }

        public string GetSummary()
        {
            var s = "";
            foreach (var p in productPool)
            {
                s += "Name: " +  p.GetType().Name + "\n";
            }
            var AllStorages = storageRegistry.Values
                .OrderBy(p => p.Products.Sum(f => f.Price));

            StringBuilder final = new StringBuilder();
            foreach (var storage in AllStorages)
            {
                final.Append($"{storage.Name}:");
                double totalMoney = storage.Products.Sum(p => p.Price);
                final.Append($"Storage Worth: {totalMoney:F2}");
            }
            return final.ToString();
        }
    }
}
