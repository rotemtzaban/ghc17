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

                foreach (var order in input.Orders)
                {
                    foreach (var product in order.ProductsInOrder)
                    {
                        for (int i = 0; i < order.ProductsInOrder.Count; i++)
                        {
                            if (order.ProductsInOrder[i] == 0)
                                continue;

                            foreach (var warehouse in input.Warehouses)
                            {
                                if (warehouse.NumberOfItemsForProduct[(int)product] >= order.ProductsInOrder[(int)product))
                                    foreach (var item2 in drones)
                                    {
                                    }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
