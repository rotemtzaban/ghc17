using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Warehouse
    {
        private static int s_ID = 0;

        public int ID { get; set;}

        public Coordinate Location { get; set; }

        public Dictionary<Product, int> Products { get; set; }

        public Warehouse()
        {
            this.ID = s_ID++;
        }

        public Warehouse (Warehouse other)
        {
            this.ID = other.ID;
            this.Location = other.Location;
            this.Products = new Dictionary<DronesProblem.Product, int>();

            foreach (var item in Products)
            {
                Product clone = new DronesProblem.Product(item.Key);
                this.Products.Add(clone, item.Value);
            }
        }

        public override bool Equals(object obj)
        {
            Warehouse other = obj as Warehouse;
            if (other == null)
            {
                return false;
            }

            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}
