﻿using System;
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

        public readonly IReadOnlyCollection<Vehicle> Garage;
        public readonly IReadOnlyCollection<Product> Products;

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.Garage = new Vehicle[garageSlots];//corrected here 
            this.Products = new Product[] { };
            /*for (int i = 0; i < vehicles.Count(); i++)
            {
                Garage.ToArray()[i] = vehicles.ToArray()[i];
            }*/
            this.Garage = new ReadOnlyCollection<Vehicle>((IList<Vehicle>)vehicles);

        }

        // -- Methods to use -- 


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

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var sentVehicle = GetVehicle(garageSlot);

            var freeSlotInGarage = deliveryLocation.Garage.Any(p => p == null);
            if (freeSlotInGarage == false)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            var slot = Garage.ToArray()[garageSlot] = null;
            int freedSlot = Convert.ToInt32(slot);
            return freedSlot;
        }

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