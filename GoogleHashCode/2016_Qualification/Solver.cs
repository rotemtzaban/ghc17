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

            while (true)
            {
                bool assignedTask = false;

                foreach (var order in OrderOrders(input.Orders))
                {
                    bool orderFailed = false;

                    foreach (var product in order.ProductsInOrder)
                    {
                        for (int i = 0; i < order.ProductsInOrder.Count; i++)
                        {
                            if (order.ProductsInOrder[i] == 0)
                                continue;

                            Drone selectedDrone = null;
                            Warehouse selectedWarehouse = null;
                            int minTime = int.MaxValue;
                            foreach (var warehouse in input.Warehouses)
                            {
                                if (warehouse.NumberOfItemsForProduct[(int)product] >= order.ProductsInOrder[(int)product])
                                    foreach (var drone in drones)
                                    {
                                        var time = drone.CurrentTime + 
                                            Math.Ceiling(drone.CurrentPosition.CalcEucledianDistance(new MatrixCoordinate(0, 0))) + 1 +
                                            Math.Ceiling(new MatrixCoordinate(0, 0).CalcEucledianDistance(new MatrixCoordinate(0,0))) + 1;

                                        if (time < minTime)
                                        {
                                            minTime = (int)time;
                                            selectedDrone = drone;
                                            selectedWarehouse = warehouse;
                                        }
                                    }
                            }

                            if (selectedDrone == null)
                                break;
                            else
                            {
                                var time = selectedDrone.CurrentTime +
                                            Math.Ceiling(selectedDrone.CurrentPosition.CalcEucledianDistance(new MatrixCoordinate(0, 0))) + 1 +
                                            Math.Ceiling(new MatrixCoordinate(0, 0).CalcEucledianDistance(new MatrixCoordinate(0, 0))) + 1;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private List<Order> OrderOrders(List<Order> orders)
        {
            return orders.OrderBy(_ => _.ProductsInOrder.Count(__ => __ != 0)).ToList();
        }
    }
}
