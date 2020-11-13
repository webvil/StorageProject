using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StorageMaster
{
    public abstract class Storage
    {
        //private Vehicle[] VehicleInGarage;
        //private List<Product> StorageProducts = new List<Product>();

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int GarageSlots { get; set; }
        public bool IsFull
        {
            get
            {
                var productsWeight = 0.0;
                foreach (var vehicle in Garage)
                {
                    foreach (var product in vehicle.Trunk)
                    {
                        productsWeight += product.Weight;
                    }
                }
                return productsWeight >= Capacity;

            }
        }
        Vehicle[] garage;
        public IReadOnlyCollection<Vehicle> Garage;
        public IReadOnlyCollection<Product> Products;

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.Products = new Product[] { };
            this.garage = new Vehicle[GarageSlots];

            for (int i = 0; i < vehicles.Count(); i++)
            {
                this.garage[i] = vehicles.ToList()[i];
            }
            this.Garage = new ReadOnlyCollection<Vehicle>((IList<Vehicle>)this.garage);

        }

        // -- Methods to use -- 

        /// <summary>
        /// If the provided garage slot number is equal to or larger than the garage slots, 
        /// throw an InvalidOperationException with the message "Invalid garage slot!". 
        /// If the garage slot is empty, throw an InvalidOperationException with the message 
        /// "No vehicle in this garage slot!" 
        /// The method returns the retrieved vehicle.
        /// </summary>
        /// <param name="garageSlot"></param>
        /// <returns></returns>
        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.GarageSlots) 
            {
                throw new InvalidOperationException("InValid garage slot!");
            }
            else if (Garage.ElementAt(garageSlot) == null)
            {
                throw new InvalidOperationException("No vehicle in this slot!");
            }

            return Garage.ElementAt(garageSlot);
        }
        /// <summary>
        ///     Gets the vehicle from the specified garage slot 
        ///     (and delegates the validation of the garage slot to the GetVehicle method). 
        ///     Then, the method checks if there are any free garage slots. 
        ///     A free garage slot is denoted by a null value. 
        ///     If there is no free garage slot, 
        ///     throw an InvalidOperationException with the message "No room in garage!". 
        ///     Then, the garage slot in the source storage is freed 
        ///     and the vehicle is added to the first free garage slot. 
        ///     The method returns the garage slot the vehicle was assigned when it was transferred.        
        ///     /// </summary>
        /// <param name="garageSlot"></param>
        /// <param name="deliveryLocation"></param>
        /// <returns></returns>
        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var sentVehicle = GetVehicle(garageSlot);

            var freeSlotInGarage = deliveryLocation.Garage.Any(p => p == null);
            if (freeSlotInGarage == false)
            {
                throw new InvalidOperationException("No room in garage!");
            }
            else
            {
                List<Vehicle> vehiclesInGarage = deliveryLocation.Garage.ToList();
                for (int i = 0; i < vehiclesInGarage.Count; i++)
                {
                    if (vehicles[i] == null)
                    {
                        vehiclesInGarage[i] = sentVehicle;
                        
                        return i;
                    }

                }                
                    

            }
            
            return 0;
        }
        /// <summary>
        /// If the storage is full, throw an InvalidOperationException with the "Storage is full!"
        /// Gets the vehicle from the specified garage slot 
        /// (and delegates the validation of the garage slot to the GetVehicle method). 
        /// Then, until the vehicle empties, or the storage fills up, 
        /// the vehicle’s products are unpacked and are added to the storage’s products. 
        /// The method returns the number of unloaded products.
        /// </summary>
        /// <param name="garageSlot"></param>
        /// <returns></returns>
        public int UnloadVehicle(int garageSlot)
        {
            if (IsFull) throw new InvalidOperationException("Storage is full!");

            var VehicleToGet = GetVehicle(garageSlot);
            var UnloadedProducts = 0;

            while (!IsFull && !VehicleToGet.IsEmpty)
            {
                var temp = VehicleToGet.UnLoad();
                Products.ToList().Add(temp);
                UnloadedProducts++;
            }
            return UnloadedProducts;
        }

    }
}
