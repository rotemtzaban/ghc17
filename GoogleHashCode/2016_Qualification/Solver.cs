using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
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

                                if (time < minTime && time < input.NumberOfIterations)
                                {
                                    minTime = (int)time;
                                    selectedDrone = drone;
                                    selectedWarehouse = warehouse;
                                }
                            }
                    }

                    if (selectedDrone == null)
                    {
                        SolverHelper.CancelOrders(drones, output);
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

                        output.Add(new Load(selectedDrone.Index, order.Index, product.Key, product.Value, firstOrderTime, lastOrderTime));
                        output.Add(new Deliver(selectedDrone.Index, order.Index, product.Key, product.Value, lastOrderTime, selectedDrone.CurrentTime));
                    }
                }
            }

            return output;
        }
    }
}
