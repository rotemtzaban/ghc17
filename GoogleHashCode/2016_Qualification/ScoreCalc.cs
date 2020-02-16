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
        private static bool NotCompleted(Order order)
        {
            return order.ProductsInOrder.Any(pair => pair.Value != 0);
        }

        public static List<Order> OrderOrders(ProblemInput input, List<Drone> drones)
        {
            return input.Orders.Select(_ => _.Clone()).Where(NotCompleted).OrderBy(order =>
            {
                Warehouse bestWarehouse = null;
                Drone bestDrone = null;
                var bestScore = double.MinValue;
                foreach (var warehouse in input.Warehouses)
                {
                    foreach (var drone in drones)
                    {
                        var score = GetScore(order, warehouse, drone, input);
                        if (score > bestScore)
                        {
                            bestDrone = drone;
                            bestWarehouse = warehouse;
                            bestScore = score;
                        }
                    }
                }

                // HandleOrder(bestWarehouse, bestDrone, order, input, output);

                return bestScore;
                //int numOfUsedDrones = 0;
                //MatrixCoordinate coordinate = new MatrixCoordinate();
                //foreach (var product in order.ProductsInOrder)
                //{
                //    double dronesForProducts = (product.Value * input.Products[product.Key].Weight) / input.MaxDrownLoad;
                //    numOfUsedDrones += (int)Math.Ceiling(dronesForProducts);
                //    coordinate.CalcEucledianDistance(order.Coordinate);
                //}

                //return numOfUsedDrones;
            }).ToList();
        }

        public static double GetScore(Order order, Warehouse warehouse, Drone drone, ProblemInput input)
        {
            var weight = 0;
            var numberOfProducts = 0;
            foreach (var product in order.ProductsInOrder)
            {
                var count = Math.Min(product.Value, warehouse.NumberOfItemsForProduct[product.Key]);
                count = Math.Min(count, (input.MaxDrownLoad - weight) / input.Products[product.Key].Weight);
                weight += count * input.Products[product.Key].Weight;
                if (count > 0)
                {
                    numberOfProducts++;
                }
            }

            var denom = drone.CurrentPosition.CalcEucledianDistance(warehouse.Coordinate) +
                        warehouse.Coordinate.CalcEucledianDistance(order.Coordinate) + drone.CurrentTime + numberOfProducts;

            return denom;
        }

        public static void CancelOrders(List<Drone> drones, ProblemOutput output)
        {
            if (drones.Count != 0)
            {
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