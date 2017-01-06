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
            List<PizzaSlice> newSlices = new List<PizzaSlice>();

            foreach (PizzaSlice slice in slices)
            {
                PizzaSlice newSlice = TryEnlargeSlice(pizzaCells, slice, slice.Width, slice.Height);
                newSlices.Add(newSlice);
            }
        }

		#endregion

		#region Private Methods

		private void TryEnlargeSlice(PizzaSlice slice)
        {
            bool enlarged = true;
            while (enlarged)
            {
                PizzaSlice newSlice;
                if (TryEnlargedSliceByHeight(pizzaCells, slice, out newSlice))
                {
                    enlarged = true;
                    slice = newSlice;
                }
                else if (TryEnlargedSliceByWidth(pizzaCells, slice, out newSlice))
                {
                    enlarged = true;
                    slice = newSlice;
                }
                else
                {
                    enlarged = false;
                }
            }

            return slice;
        }

        private bool TryEnlargedSliceByWidth(bool[,] pizzaCells, PizzaSlice slice, out PizzaSlice newSlice)
        {
            if (slice.Height * (slice.Width + 1) > Pizza.MaxSliceSize)
            {
                newSlice = slice;
                return false;
            }

            bool succeed = true;
            if (slice.TopLeft.X != 0)
            {
                for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
                {
                    if (pizzaCells[slice.TopLeft.X - 1, 0])
                    {
                        succeed = false;
                        break;
                    }
                }

                if (succeed)
                {
                    Coordinate newTopLeft = new Coordinate(slice.TopLeft.X - 1, slice.TopLeft.Y);
                    newSlice = new PizzaSlice(newTopLeft, slice.BottomRight);
                    return true;
                }
            }

            if (slice.BottomRight.X != Pizza.XLength - 1)
            {
                for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
                {
                    if (pizzaCells[slice.BottomRight.X + 1, y])
                    {
                        succeed = false;
                        break;
                    }
                }

                if (succeed)
                {
                    Coordinate newBottomRight = new Coordinate(slice.BottomRight.X + 1, slice.BottomRight.Y);
                    newSlice = new PizzaSlice(slice.TopLeft, newBottomRight);
                    return true;
                }
            }

            newSlice = slice;
            return false;
        }

        private bool TryEnlargedSliceByHeight(bool[,] pizzaCells, PizzaSlice slice, out PizzaSlice newSlice)
        {
            if (slice.Width * (slice.Height + 1) > Pizza.MaxSliceSize)
            {
                newSlice = slice;
                return false;
            }

            bool succeed = true;
            if (slice.TopLeft.Y != 0)
            {
                for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
                {
                    if (pizzaCells[x, slice.TopLeft.Y - 1])
                    {
                        succeed = false;
                        break;
                    }
                }

                if (succeed)
                {
                    Coordinate newTopLeft = new Coordinate(slice.TopLeft.X, slice.TopLeft.Y - 1);
                    newSlice = new PizzaSlice(newTopLeft, slice.BottomRight);
                    return true;
                }
            }

            if (slice.BottomRight.Y != Pizza.YLength - 1)
            {
                for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
                {
                    if (pizzaCells[x, slice.BottomRight.Y + 1])
                    {
                        succeed = false;
                        break;
                    }
                }

                if (succeed)
                {
                    Coordinate newBottomRight = new Coordinate(slice.BottomRight.X, slice.BottomRight.Y + 1);
                    newSlice = new PizzaSlice(slice.TopLeft, newBottomRight);
                    return true;
                }
            }

            newSlice = slice;
            return false;
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