using System;
using System.Collections;
using System.Collections.Generic;

namespace Pizza_problem
{
	public class PizzaSolverBase
	{
		protected readonly PizzaParams Pizza;

		private readonly int[,] tomatoTable;
		private readonly int[,] mushroomTable;

		public PizzaSolverBase(PizzaParams pizza)
		{
			Pizza = pizza;
			tomatoTable = CreateCountTable(Ingredient.Tomato);
			mushroomTable = CreateCountTable(Ingredient.Mushroom);
		}

		private int[,] CreateCountTable(Ingredient ingredient)
		{
			int[,] table = new int[Pizza.XLength, Pizza.YLength];
			table[0, 0] = Pizza.PizzaIngredients[0, 0] == ingredient ? 1 : 0;
			for (int x = 1; x < Pizza.XLength; x++)
			{
				var ingCount = Pizza.PizzaIngredients[x, 0] == ingredient ? 1 : 0;
				table[x, 0] = table[x - 1, 0] + ingCount;
			}
			for (int y = 1; y < Pizza.YLength; y++)
			{
				var ingCount = Pizza.PizzaIngredients[0, y] == ingredient ? 1 : 0;
				table[0, y] = table[0, y - 1] + ingCount;
			}
			for (int x = 1; x < Pizza.XLength; x++)
			{
				for (int y = 1; y < Pizza.YLength; y++)
				{
					var ingCount = Pizza.PizzaIngredients[x, y] == ingredient ? 1 : 0;
					table[x, y] = ingCount + table[x - 1, y] + table[x, y - 1] - table[x - 1, y - 1];
				}
			}

			return table;
		}

		public int GetMushroomsInSlice(PizzaSlice slice)
		{
			return GetCountInSlice(mushroomTable, slice);
		}

		public int GetTomatoInSlice(PizzaSlice slice)
		{
			return GetCountInSlice(tomatoTable, slice);
		}

		private static int GetCountInSlice(int[,] table, PizzaSlice slice)
		{
			var totalSum = table[slice.BottomRight.X, slice.BottomRight.Y];
			var topSum = slice.TopLeft.Y == 0 ? 0 : table[slice.BottomRight.X, slice.TopLeft.Y - 1];
			var leftSum = slice.TopLeft.X == 0 ? 0 : table[slice.TopLeft.X - 1, slice.BottomRight.Y];
			var topLeftSum = slice.TopLeft.X == 0 || slice.TopLeft.Y == 0 ? 0 : table[slice.TopLeft.X - 1, slice.TopLeft.Y - 1];

			return totalSum - leftSum - topSum + topLeftSum;
		}

		public bool IsSliceValid(PizzaSlice slice)
		{
			return !IsSliceTooLarge(slice) && IsEnoughIngredients(slice);
		}

		public bool IsEnoughIngredients(PizzaSlice slice)
		{
			int tomatoCount = 0;
			int mushroomCount = 0;
			for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
			{
				for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
				{
					if (Pizza.PizzaIngredients[x, y] == Ingredient.Mushroom)
						mushroomCount++;
					if (Pizza.PizzaIngredients[x, y] == Ingredient.Tomato)
						tomatoCount++;

					if (mushroomCount >= Pizza.MinIngredientNum && tomatoCount >= Pizza.MinIngredientNum)
						return true;
				}
			}

			return false;
		}

		public bool IsSliceTooLarge(PizzaSlice slice)
		{
			return slice.Size > Pizza.MaxSliceSize;
		}

        public void EnlargeSlices(IEnumerable<PizzaSlice> slices)
        {
            bool[,] pizzaCells = GetSlicesArray(slices);

            foreach (PizzaSlice slice in slices)
            {
                TryEnlargeSlice(slice);
            }
        }

        private void TryEnlargeSlice(PizzaSlice slice)
        {
            throw new NotImplementedException();
        }

        private bool[,] GetSlicesArray(IEnumerable<PizzaSlice> slices)
        {
            bool[,] pizzaCells = new bool[Pizza.XLength, Pizza.YLength];

            foreach (PizzaSlice slice in slices)
            {
                for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
                {
                    for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
                    {
                        pizzaCells[x, y] = true;
                    }
                }
            }

            return pizzaCells;
        }
    }
}