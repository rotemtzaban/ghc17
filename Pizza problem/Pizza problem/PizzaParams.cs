using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public class PizzaParams
	{
		public int XLength { get; private set; }
		public int YLength { get; private set; }
        public Ingredient[,] PizzaIngredients;
        public int MaxSliceSize { get; set; }
        public int MinIngredientNum { get; set; }

        public PizzaParams(int rows, int columns, int minIngredientNum, int maxSliceSize)
        {
	        this.XLength = columns;
	        this.YLength = rows;

            this.MaxSliceSize = maxSliceSize;
            this.MinIngredientNum = minIngredientNum;
            this.PizzaIngredients = new Ingredient[columns, rows];
        }
    }
}
