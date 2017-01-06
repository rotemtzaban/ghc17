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
			if (slice.Size > Pizza.MaxSliceSize)
				return false;

			int tomatoCount = 0;
			int mushroomCount = 0;
			for (int x = slice.TopLeft.X; x < slice.BottomRight.X; x++)
			{
				for (int y = slice.TopLeft.Y; y < slice.BottomRight.Y; y++)
				{
				}
			}

			return false;
		}
	}
}