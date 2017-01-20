using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DronesProblem
{
    public class DronesParser : ParserBase<DronesInput>
    {
        protected override DronesInput ParseFromStream(TextReader reader)
        {
            DronesInput input = new DronesInput();

            string line = reader.ReadLine();
            string[] firstLine = line.Split(' ');
            input.NumOfRows = int.Parse(firstLine[0]);
            input.NumOfColumns = int.Parse(firstLine[1]);
            input.NumOfTurns = int.Parse(firstLine[3]);
            input.MaxWeight = int.Parse(firstLine[4]);

            int numOfDrones = int.Parse(firstLine[2]);
            input.Drones = new List<Drone>();
            for (int i = 0; i < numOfDrones; i++)
            {
                input.Drones.Add(new Drone(i));
            }

            int numOfProductTypes = int.Parse(reader.ReadLine());
            int n = 0;
            input.Products = reader.ReadLine().Split(' ').Select(weight => new Product(n++, weight)).ToList();

            int numOfWareHouses = int.Parse(reader.ReadLine());
            input.WareHouses = new List<Warehouse>();

            for (int i = 0; i < numOfWareHouses; i++)
            {
                Warehouse current = new Warehouse(i);
                string[] locationAsString = reader.ReadLine().Split(' ');
                current.Location = new Coordinate(int.Parse(locationAsString[0]), int.Parse(locationAsString[1]));

                current.Products = new Dictionary<Product, int>();
                string[] itemsAsString = reader.ReadLine().Split(' ');
                for (int index = 0; index < numOfProductTypes; index++)
                {
                    int count = int.Parse(itemsAsString[index]);
                    current.Products.Add(input.Products[index], count);
                }

                input.WareHouses.Add(current);
            }

            int numOfOrders = int.Parse(reader.ReadLine());
            input.Orders = new List<Order>();

            for (int index = 0; index < numOfOrders; index++)
            {
                Order order = new Order(index);
                order.WantedProducts = new List<Product>();
                string[] locationAsString = reader.ReadLine().Split(' ');
                order.Location = new Coordinate(int.Parse(locationAsString[0]), int.Parse(locationAsString[1]));

                int numOfItems = int.Parse(reader.ReadLine());
                string[] items = reader.ReadLine().Split(' ');

                for (int i = 0; i < items.Length; i++)
                {
                    order.WantedProducts.Add(input.Products[int.Parse(items[i])]);
                }

                input.Orders.Add(order);
            }

            return input;
        }
    }
}
