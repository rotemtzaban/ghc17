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
            foreach (var order in SolverHelper.OrderOrders(input))
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

                    HandleOrder(bestWarehouse, bestDrone, order, output);
                }

                foreach (var product in order.ProductsInOrder)
                {
                    if (product.Value * input.Products[product.Key].Weight > input.MaxDrownLoad)
                    {
                        // throw new NotSupportedException();
                        break;
                    }

                    Drone selectedDrone = null;
                    Warehouse selectedWarehouse = null;
                    int minTime = int.MaxValue;



                    foreach (var warehouse in input.Warehouses)
                    {
                        // TODO: Handle drone can't load all items
                        if (warehouse.NumberOfItemsForProduct[product.Key] > product.Value)
                            foreach (var drone in drones)
                            {
                                var time = drone.CurrentTime +
                                    Math.Ceiling(drone.CurrentPosition.CalcEucledianDistance(warehouse.Coordinate)) + 1 +
                                    Math.Ceiling(warehouse.Coordinate.CalcEucledianDistance(order.Coordinate)) + 1;

                                if (time < minTime)
                                {
                                    minTime = (int)time;
                                    selectedDrone = drone;
                                    selectedWarehouse = warehouse;
                                }
                            }
                    }

                    if (selectedDrone == null)
                    {
                        SolverHelper.CancelOrders(drones);
                        break;
                    }
                    else
                    {
                        var time = selectedDrone.CurrentTime +
                                    Math.Ceiling(selectedDrone.CurrentPosition.CalcEucledianDistance(selectedWarehouse.Coordinate)) + 1 +
                                    Math.Ceiling(selectedWarehouse.Coordinate.CalcEucledianDistance(order.Coordinate)) + 1;

                        int firstOrderTime = selectedDrone.CurrentTime;
                        int lastOrderTime = selectedDrone.CurrentTime - 1 - (int)Math.Ceiling(selectedWarehouse.Coordinate.CalcEucledianDistance(order.Coordinate));
                        selectedDrone.CurrentTime += (int)time;
                        selectedDrone.CurrentPosition = new MatrixCoordinate(0, 0);

                        output.Add(new Deliver(selectedDrone.Index, order.Index, product.Key, product.Value, firstOrderTime, lastOrderTime));
                        output.Add(new Unload(selectedDrone.Index, order.Index, product.Key, product.Value, lastOrderTime, selectedDrone.CurrentTime));
                    }
                }
            }

            return output;
        }

        private void HandleOrder(Warehouse warehouse, Drone drone, Order order, ProblemInput input, ProblemOutput output)
        {
            var weight = 0;
            var numberOfProducts = 0;
            
            List<>

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
            return order.ProductsInOrder.Any(pair => pair.Value != 0)
        }
    }
}
