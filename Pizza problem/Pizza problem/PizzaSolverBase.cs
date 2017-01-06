using System;
using System.Collections;
using System.Collections.Generic;

namespace Pizza_problem
{
	public class PizzaSolverBase
	{
		protected readonly PizzaParams Pizza;

		public PizzaSolverBase(PizzaParams pizza)
		{
			Pizza = pizza;
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