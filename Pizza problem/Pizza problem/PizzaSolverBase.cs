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
	}
}