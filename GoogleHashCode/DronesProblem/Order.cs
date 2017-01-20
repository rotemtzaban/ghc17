using HashCodeCommon;
using HashCodeCommon.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Order : IndexedObject
    {
        public Coordinate Location { get; set; }

        public List<Product> WantedProducts { get; set; }

        public Order(int index)
            :base (index)
        {
            if (index == 1070)
            {
                int n = 0;
            }
        }

        public Order(Order other)
            :this (other.Index)
        {
            this.Location = other.Location;
            this.WantedProducts = new List<Product>();

            foreach (var item in other.WantedProducts)
            {
                this.WantedProducts.Add(new Product(item));
            }
        }
    }
}
