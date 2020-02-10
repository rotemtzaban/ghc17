using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public static class SolverHelper
    {
        public static List<Order> OrderOrders(ProblemInput input)
        {
            return input.Orders.OrderBy(order =>
            {
                int numOfUsedDrones = 0;
                MatrixCoordinate coordinate = new MatrixCoordinate();
                foreach (var product in order.ProductsInOrder)
                {
                    double dronesForProducts = (product.Value * input.Products[product.Key].Weight) / 200;
                    numOfUsedDrones += (int)Math.Ceiling(dronesForProducts);
                    coordinate.CalcEucledianDistance(order.Coordinate);
                }

                return numOfUsedDrones;
            }).ToList();
        }

        public static void CancelOrders(List<Drone> drones, ProblemOutput output)
        {
            if (drones.Count != 0)
            {
                Console.WriteLine(drones.Count);
                return;
            }

            for (int i = 0; i < drones.Count; i++)
            {
                int startIndex = output.Count - 2 - i * 2;
                drones[drones.Count - 1 - i].CurrentTime = output[startIndex].Time;
                drones[drones.Count - 1 - i].CurrentPosition= output[startIndex].StartPosition;
            }

            for (int i = 0; i < 2 * drones.Count; i++)
            {
                output.RemoveAt(output.Count - 1 - i);
            }
        }
    }
}