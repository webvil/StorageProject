using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageMaster;

namespace Controller
{
    class StartUp
    {
        static void Main(string[] args)
        {



            /*var storage = c.RegisterStorage("Warehouse", "AmozonWareHouse");


            foreach (var vehicle in storage.Garage)
            {
                if (vehicle == null)
                {
                    Console.WriteLine("Vehicle is null");
                }
                else
                {
                    Console.WriteLine(vehicle.GetType());
                }

            }

            Console.ReadLine();*/
            var c = new StoregeMaster();

            while (true)
            {

                string[] input = Console.ReadLine().Split();
                if (input[0] == "Q" || input[0] == "q" || input[0] == "exit")
                {
                    break;
                }
                if (input[0] == "RegisterStorage")
                {
                    try
                    {

                        string s = c.RegisterStorage(input[1], input[2]);
                        Console.WriteLine(s);

                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                else if (input[0] == "AddProduct")
                {
                    try
                    {

                        string s = c.AddProduct(input[1], int.Parse(input[2]));
                        Console.WriteLine(s);

                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (input[0] == "SelectVehicle")
                {
                    try
                    {

                        string s = c.SelectVehicle(input[1], int.Parse(input[2]));
                        Console.WriteLine(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (input[0] == "LoadVehicle")
                {
                    try
                    {

                        var products = input.ToList();
                        products.RemoveAt(0);



                        string s = c.LoadVehicle(products);
                        Console.WriteLine(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (input[0] == "SendVehicleTo")
                {
                    try
                    {

                        string s = c.SendVehicleTo(input[1], int.Parse(input[2]), input[3]);
                        Console.WriteLine(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else if (input[0] == "UnloadVehicle")
                {
                    try
                    {

                        string s = c.UnloadVehicle(input[1], int.Parse(input[2]));
                        Console.WriteLine(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                else if (input[0] == "GetSummary")
                {
                    try
                    {

                        string s = c.GetSummary();
                        Console.WriteLine(s);
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }

        }

    }
}
