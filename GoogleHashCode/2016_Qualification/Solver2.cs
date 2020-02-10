using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public class Solver2 : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            List<Drone> drones = new List<Drone>();
            for (int i = 0; i < input.NumberOfDrones; i++)
            {
                drones.Add(new Drone(i));
            }

            ProblemOutput output = new ProblemOutput();
            foreach (var order in SolverHelper.OrderOrders(input, drones))
            {
                List<Drone> usedDrones = new List<Drone>();

                while (NotCompleted(order))
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

                    HandleOrder(bestWarehouse, bestDrone, order, input, output);
                }
            }

            return output;
        }

        private void HandleOrder(Warehouse warehouse, Drone drone, Order order, ProblemInput input, ProblemOutput output)
        {
            var weight = 0;
            
            List<Load> loads = new List<Load>();
            List<Deliver> delivers = new List<Deliver>();
            
            foreach (var productId in order.ProductsInOrder.Keys)
            {
                var product = new KeyValuePair<int, int>(productId,order.ProductsInOrder[productId]);
                var count = Math.Min(product.Value, warehouse.NumberOfItemsForProduct[product.Key]);
                count = Math.Min(count, (input.MaxDrownLoad - weight) / input.Products[product.Key].Weight);
                weight += count * input.Products[product.Key].Weight;

                if (count == 0) continue;

                var toWareHouse = (int)Math.Ceiling(drone.CurrentPosition.CalcEucledianDistance(warehouse.Coordinate));
                var endTime = drone.CurrentTime + toWareHouse + 1;
                loads.Add(new Load(drone.Index, warehouse.Index, product.Key, count, drone.CurrentTime,
                    endTime, drone.CurrentPosition));

                drone.CurrentTime = endTime;
                drone.CurrentPosition = warehouse.Coordinate;
                warehouse.NumberOfItemsForProduct[product.Key] -= count;
                order.ProductsInOrder[product.Key] -= count;
            }

            foreach (var load in loads)
            {
                var toOrder = (int)Math.Ceiling(drone.CurrentPosition.CalcEucledianDistance(order.Coordinate));
                var endTime = drone.CurrentTime + toOrder + 1;
                delivers.Add(new Deliver(drone.Index, order.Index, load.ProductId, load.ProductId,drone.CurrentTime, endTime));
                drone.CurrentTime = endTime;
                drone.CurrentPosition = order.Coordinate;
            }

            output.AddRange(loads);
            output.AddRange(delivers);
        }

        private double GetScore(Order order, Warehouse warehouse, Drone drone, ProblemInput input)
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

            return weight / denom;
        }

        private static bool NotCompleted(Order order)
        {
            return order.ProductsInOrder.Any(pair => pair.Value != 0);
        }
    }
}
