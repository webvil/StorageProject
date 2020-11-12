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
        List<(string name, double price)> productPool = new List<(string name, double price)>();
        Vehicle currentVehicle;
        public string AddProduct(string type, double price)
        {

            try
            {
                productPool.Add((type, price));     

            }
            catch (Exception)
            {

                throw new InvalidOperationException("Invalid product type");
                
            }
            
              return $"Added {type} to pool.";
                    
        }
           
        
        public string RegisterStorage(string type, string name)
        {
            if (storageRegistry.ContainsKey(name))
            {

                return "A store with the same name already exists";
                //return default(Storage);
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

            return $"Registered {name}";
            //return storageRegistry.ToList()[0].Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storageName"></param>
        /// <param name="garageSlot"></param>
        /// <returns></returns>
        public string SelectVehicle(string storageName, int garageSlot)
        {

            //set the current vehicle
            var storage = storageRegistry[storageName];
            currentVehicle = storage.GetVehicle(garageSlot);
            return $"Selected { currentVehicle.GetType()}";
            
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            
            int count = 0;
            foreach (var name in productNames)
            {
                foreach (var p in productPool)
                {
                    Console.WriteLine($"Name = { p.name }");
                    if (p.name == name)
                    {
                        switch (name)
                        {
                            case "Gpu":
                                currentVehicle.LoadProduct(new Gpu(p.price));
                                break;
                            case "Ram":
                                currentVehicle.LoadProduct(new Ram(p.price));
                                break;
                            case "HardDrive":
                                currentVehicle.LoadProduct(new HardDrive(p.price));
                                break;
                            case "SolidStateDrive":
                                currentVehicle.LoadProduct(new SolidStateDrive(p.price));
                                break;

                            default:
                                break;
                        }
                        count++;
                        productPool.Remove(p);
                        break;
                    }
                    else
                    {
                        throw new InvalidOperationException(name + " is out of stock!");

                    }

                }

            }
            return $"Loaded { count}/{ productNames.Count()} products into { currentVehicle.GetType() }";
            
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            var vehicle = SelectVehicle(sourceName, sourceGarageSlot);
            var destinationStorage = storageRegistry[destinationName];
            var sourceStorage = storageRegistry[sourceName];
            int openSlot = 0;
            for (int i = 0; i < destinationStorage.Garage.Count; i++)
            {
                if (destinationStorage.Garage.ToArray()[i] == null)
                {
                    openSlot = i;
                    destinationStorage.SendVehicleTo(openSlot, destinationStorage);
                    break;
                }
                    
            }
            //currentVehicle 

            if (sourceStorage == null) throw new InvalidOperationException("Invalid source storage!");
            if (destinationStorage == null) throw new InvalidOperationException("Invalid destination storage!");
            return $"Sent {destinationStorage.GetType()} to {destinationName} (slot {openSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var storage = storageRegistry[storageName];
            var productsInVehicle = storage.GetVehicle(garageSlot).Trunk.Count;
            int unloadedProductsCount = storage.UnloadVehicle(garageSlot);
            return $"Unloaded { unloadedProductsCount}/{ productsInVehicle} products at { storageName}";
            
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = storageRegistry[storageName];
            var productsCount = storage.Products.Count;
            IEnumerable<Product> products = storage.Products.ToArray();
            /*products.OrderBy<int, Product>(p => p.GetType()).
                GroupBy(p => p.Name).ToList();
            return "Stock ({0}/{1}): [{2}]\n, { productsCount}, productsCount, productsCount"; 
                + "Garage: [{0}]";*/
            return "string";
        }

        public string GetSummary()
        {
            var s = "";
            foreach (var item in productPool)
            {
                s += "Name: " + item.name + "\n";
            }
            return s;
        }
    }
}
