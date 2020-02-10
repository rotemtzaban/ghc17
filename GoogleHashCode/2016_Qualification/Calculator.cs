using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Qualification
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public Dictionary<int, int> OrderIdToMaxTry = new Dictionary<int, int>();
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            long score = 0;
            foreach (var command in output)
            {
                if (command is Deliver)
                {
                    Deliver deliver = (Deliver) command;
                    Order order = input.Orders[deliver.OrderId];
                    
                    if (!OrderIdToMaxTry.ContainsKey(order.Index))
                    {
                        OrderIdToMaxTry[order.Index] = deliver.EndTime;
                    }
                    else
                    {
                        OrderIdToMaxTry[order.Index] = Math.Max(deliver.EndTime, OrderIdToMaxTry[order.Index]);
                    }

                    if (input.Orders[deliver.OrderId].ProductsInOrder[deliver.ProductId] - deliver.CountOfProducts == 0)
                    {
                        input.Orders[deliver.OrderId].ProductsInOrder.Remove(deliver.ProductId);
                    }
                    else
                    {
                        input.Orders[deliver.OrderId].ProductsInOrder[deliver.ProductId] =
                            input.Orders[deliver.OrderId].ProductsInOrder[deliver.ProductId] - deliver.CountOfProducts;
                    }

                    if (!input.Orders[deliver.OrderId].ProductsInOrder.Any())
                    {
                        var currentScore = ((1.0 * input.NumberOfIterations - OrderIdToMaxTry[deliver.OrderId]) /
                                            input.NumberOfIterations) * 100.0;
                        score += (long) Math.Ceiling(currentScore);
                    }
                }
            }

            return score;
        }


        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
