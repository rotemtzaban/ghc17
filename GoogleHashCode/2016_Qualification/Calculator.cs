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
            foreach (var command in output)
            {
                if (command is Deliver)
                {
                    Deliver deliver = (Deliver) command;
                    Order order = input.Orders[deliver.OrderId];
                    
                    //int 
                    if (!OrderIdToMaxTry.ContainsKey(order.Index))
                    {
                        //CommandTurn
                        OrderIdToMaxTry[order.Index] = 0;//deliver.
                    }

                    if (order.ProductsInOrder[deliver.ProductId] - deliver.CountOfProducts == 0)
                    {
                        order.ProductsInOrder.Remove(deliver.ProductId);
                    }
                    else
                    {
                        order.ProductsInOrder[deliver.ProductId] =
                            order.ProductsInOrder[deliver.ProductId] - deliver.CountOfProducts;
                    }
                }
            }

            return 0;
        }


        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
