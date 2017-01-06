using System;
using System.Collections;
using System.Collections.Generic;

namespace Pizza_problem
{
	public class PizzaSolverBase
	{
		#region Fields

		protected readonly PizzaParams Pizza;

		private readonly int[,] tomatoTable;
		private readonly int[,] mushroomTable;

		#endregion

		#region C'tor

		public PizzaSolverBase(PizzaParams pizza)
		{
			Pizza = pizza;
			tomatoTable = CreateCountTable(Ingredient.Tomato);
			mushroomTable = CreateCountTable(Ingredient.Mushroom);
		}

		#endregion

		#region Public Methods

		public int GetMushroomsInSlice(PizzaSlice slice)
		{
			return GetCountInSlice(mushroomTable, slice);
		}

		public int GetTomatoInSlice(PizzaSlice slice)
		{
			return GetCountInSlice(tomatoTable, slice);
		}

		public bool IsSliceValid(PizzaSlice slice)
		{
			return !IsSliceTooLarge(slice) && IsEnoughIngredients(slice);
		}

		public bool IsEnoughIngredients(PizzaSlice slice)
		{
			return GetMushroomsInSlice(slice) >= Pizza.MinIngredientNum && GetTomatoInSlice(slice) >= Pizza.MinIngredientNum;
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

		#endregion

		#region Private Methods

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

		private static int GetCountInSlice(int[,] table, PizzaSlice slice)
		{
			var totalSum = table[slice.BottomRight.X, slice.BottomRight.Y];
			var topSum = slice.TopLeft.Y == 0 ? 0 : table[slice.BottomRight.X, slice.TopLeft.Y - 1];
			var leftSum = slice.TopLeft.X == 0 ? 0 : table[slice.TopLeft.X - 1, slice.BottomRight.Y];
			var topLeftSum = slice.TopLeft.X == 0 || slice.TopLeft.Y == 0 ? 0 : table[slice.TopLeft.X - 1, slice.TopLeft.Y - 1];

			return totalSum - leftSum - topSum + topLeftSum;
		}

		#endregion
    }
}