using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Pizza_problem
{
    public abstract class PizzaSolverBase
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

        public IEnumerable<PizzaSlice> Solve()
        {
            IEnumerable<PizzaSlice> slices = Solve(0, 0, Pizza.XLength - 1, Pizza.YLength - 1);

            Random rand = new Random();
            IEnumerable<PizzaSlice> maxSlices = this.EnlargeSlices(slices.ToList(), rand);
            int maxScore = ScoreCalc.GetScoreFromSlices(maxSlices);

            for (int index = 0; index < 100; index++)
            {
                IEnumerable<PizzaSlice> newSlice = this.EnlargeSlices(slices.ToList(), rand);

                int score = ScoreCalc.GetScoreFromSlices(newSlice);
                if (score > maxScore)
                {
                    maxScore = score;
                    maxSlices = newSlice;
                }
            }

            return maxSlices;
        }

        protected abstract IEnumerable<PizzaSlice> Solve(int v1, int v2, int v3, int v4);

        public IEnumerable<PizzaSlice> EnlargeSlices(List<PizzaSlice> slices, Random rand)
        {
            bool[,] pizzaCells = GetUsedCells(slices);
            List<PizzaSlice> newSlices = new List<PizzaSlice>();

            while (slices.Any())
            {
                int index = rand.Next() % slices.Count;
                PizzaSlice newSlice = EnlargeSlice(pizzaCells, slices[index]);
                newSlices.Add(newSlice);
                slices.RemoveAt(index);
            }

            return newSlices;
        }

        #endregion

        #region Private Methods

        private PizzaSlice EnlargeSlice(bool[,] pizzaCells, PizzaSlice slice)
        {
            bool enlarged = true;
            while (enlarged)
            {
                PizzaSlice newSlice;
                if (TryEnlargedSliceByWidth(pizzaCells, slice, out newSlice))
                {
                    enlarged = true;
                    slice = newSlice;
                    MarkSlice(pizzaCells, slice);
                }
                else if (TryEnlargedSliceByHeight(pizzaCells, slice, out newSlice))
                {
                    enlarged = true;
                    slice = newSlice;
                    MarkSlice(pizzaCells, slice);
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

            if (slice.TopLeft.X != 0)
            {
                bool succeed = true;
                for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
                {
                    if (pizzaCells[slice.TopLeft.X - 1, y])
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
                bool succeed = true;
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

            if (slice.TopLeft.Y != 0)
            {
                bool succeed = true;
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
                bool succeed = true;
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

        private bool[,] GetUsedCells(IEnumerable<PizzaSlice> slices)
        {
            bool[,] pizzaCells = new bool[Pizza.XLength, Pizza.YLength];

            foreach (PizzaSlice slice in slices)
            {
                MarkSlice(pizzaCells, slice);
            }

            return pizzaCells;
        }

        private static void MarkSlice(bool[,] pizzaCells, PizzaSlice slice)
        {
            for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
            {
                for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
                {
                    pizzaCells[x, y] = true;
                }
            }
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