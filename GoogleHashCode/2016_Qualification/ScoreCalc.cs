using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public static class ScoreCalc
    {
        public static List<Order> OrderOrders(ProblemInput input)
        {
            foreach (var item in input.Orders)
            {

            }

            return input.Orders.OrderBy(_ => _.ProductsInOrder.Count).ToList();
        }
    }
}
