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

		protected override IEnumerable<PizzaSlice> Solve(int xStart, int yStart, int maxX, int maxY)
		{
			if (xStart > maxX || yStart > maxY)
				return new PizzaSlice[] {};

			var results = new List<PizzaSlice>();
			int xEnd = xStart;
			int yEnd = yStart;
			while (true)
			{
				var currentSlice = new PizzaSlice(xStart, yStart, xEnd, yEnd);
				if (IsSliceTooLarge(currentSlice))
				{
					results.AddRange(Solve(xEnd + 1, yStart, maxX, yEnd));
					results.AddRange(Solve(xStart, yEnd + 1, maxX, maxY));
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
					return new PizzaSlice[] {};

				if (yEnd == maxY)
					xEnd++;
				else if (xEnd == maxX)
					yEnd++;
				else if (currentSlice.Height >= currentSlice.Width)
					xEnd++;
				else
					yEnd++;
			}
		}
	}
}
