using HashCodeCommon;
using HashCodeCommon.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Warehouse : IndexedObject
    {
        public Coordinate Location { get; set; }

        public Dictionary<Product, int> Products { get; set; }

        public Warehouse(int index)
            :base (index)
        {
        }

        public Warehouse (Warehouse other)
            :this (other.Index)
        {
            this.Location = other.Location;
            this.Products = new Dictionary<DronesProblem.Product, int>();

            foreach (var item in other.Products)
            {
                Product clone = new DronesProblem.Product(item.Key);
                this.Products.Add(clone, item.Value);
            }
        }
    }
}
