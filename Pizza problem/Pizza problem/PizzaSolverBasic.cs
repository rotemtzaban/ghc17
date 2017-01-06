using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
	public class PizzaSolverBasic : PizzaSolverBase
	{
		public PizzaSolverBasic(PizzaParams pizza) : base(pizza)
		{
		}

		public IEnumerable<PizzaSlice> Solve()
		{
			return Solve(0, 0);
		}

		public IEnumerable<PizzaSlice> Solve(int xStart, int yStart)
		{
			if (xStart > Pizza.XLength || yStart > Pizza.YLength)
				return new PizzaSlice[] {};

			var results = new List<PizzaSlice>();
			int xEnd = xStart;
			int yEnd = yStart;
			while (true)
			{
				var currentSlice = new PizzaSlice(xStart, yStart, xEnd, yEnd);
				if (IsSliceTooLarge(currentSlice))
				{
					// TODO: Infinite loop if last column is invalid :(
					xStart = xEnd;
					yEnd = yStart;
					continue;
				}

				if (IsEnoughIngredients(currentSlice))
				{
					results.Add(currentSlice);
					results.AddRange(Solve(xEnd + 1, yStart));
					results.AddRange(Solve(xStart, yEnd + 1));
					return results;
				}

				if (yEnd == Pizza.YLength - 1 && xEnd == Pizza.XLength - 1)
					return new PizzaSlice[] {};

				if (yEnd == Pizza.YLength - 1)
					xEnd++;
				else if (xEnd == Pizza.XLength - 1)
					yEnd++;
				else if (currentSlice.Height >= currentSlice.Width)
					xEnd++;
				else
					yEnd++;
			}
		}
	}
}
