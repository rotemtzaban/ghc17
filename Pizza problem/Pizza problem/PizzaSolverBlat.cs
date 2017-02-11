using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public class PizzaSolverBlat : PizzaSolverBase
    {
        public PizzaSolverBlat(PizzaParams pizza) : base(pizza)
		{
        }

        protected override IEnumerable<PizzaSlice> Solve(int xStart, int yStart, int maxX, int maxY)
        {
            if (xStart > maxX || yStart > maxY)
                return new PizzaSlice[] { };

            var results = new List<PizzaSlice>();
            int xEnd = xStart;
            int yEnd = yStart;

            int xIncrement = 0;
            int yIncrement = 0;

            while (true)
            {
                var currentSlice = new PizzaSlice(xStart, yStart, xEnd, yEnd);

                if (IsSliceTooLarge(currentSlice))
                {

                    var x1 = Solve(xEnd + 1, yStart, maxX, yEnd);
                    var x2 = Solve(xStart, yEnd + 1, maxX, maxY);

                    if (x1.Count() + x2.Count() == 0)
                    {
                        if (yStart == maxY)
                            xStart++;
                        else
                            yStart++;
                        continue;
                    }

                    results.AddRange(x1);
                    results.AddRange(x2);
                    return results;
                }

                if (IsEnoughIngredients(currentSlice))
                {
                    results.Add(currentSlice);
                    results.AddRange(Solve(xEnd + 1, yStart, maxX, yEnd));
                    results.AddRange(Solve(xStart, yEnd + 1, maxX, maxY));
                    return results;
                }

                if (yEnd == maxY && xEnd == maxX)
                    return new PizzaSlice[] { };

                if (yEnd == maxY)
                {
                    xEnd++;
                    continue;
                }
                if (xEnd == maxX)
                {
                    yEnd++;
                    continue;
                }

                if (xIncrement == 0 && yIncrement == 0)
                {
                    xIncrement=1;
                    xEnd++;
                    yIncrement = 0;
                }
                else if (xIncrement == 1 && yIncrement == 0)
                {
                    xIncrement = 0;
                    xEnd--;
                    yIncrement = 1;
                    yEnd++;
                }
                else if (xIncrement == 0 && yIncrement == 1)
                {
                    xIncrement = 1;
                    xEnd++;
                    yIncrement = 1;
                }
                else if (xIncrement == 1 && yIncrement == 1)
                {
                    xIncrement = 1;
                    xEnd++;
                    yIncrement = 0;
                }
            }
        }

        private bool CheckSlice(PizzaSlice slice)
        {
            return (IsEnoughIngredients(slice) && !IsSliceTooLarge(slice));
        }
    }
}
