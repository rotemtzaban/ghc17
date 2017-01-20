using HashCodeCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesInput : IGoodCloneable<DronesInput>
    {
        public List<Drone> Drones { get; set; }

        public List<Warehouse> WareHouses { get; set; }

        public List<Product> Products { get; set; }

        public List<Order> Orders { get; set; }

        public int MaxWeight { get; set; }

        public int NumOfTurns { get; set; }

        public int NumOfRows { get; set; }

        public int NumOfColumns { get; set; }

        public DronesInput Clone()
        {
            DronesInput cloned = new DronesInput();
            cloned.MaxWeight = this.MaxWeight;
            cloned.NumOfColumns = this.NumOfColumns;
            cloned.NumOfRows = this.NumOfRows;
            cloned.NumOfTurns = this.NumOfTurns;

            cloned.Drones = new List<Drone>();
            foreach (var item in Drones)
            {
                cloned.Drones.Add(new Drone(item));
            }

            cloned.Orders = new List<Order>();
            foreach (var item in Orders)
            {
                cloned.Orders.Add(new Order(item));
            }

            cloned.Products = new List<Product>();
            foreach (var item in Products)
            {
                cloned.Products.Add(new Product(item));
            }

            cloned.WareHouses = new List<Warehouse>();
            foreach (var item in WareHouses)
            {
                cloned.WareHouses.Add(new Warehouse(item));
            }

            return cloned;
        }
    }
}
