using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public class PizzaParams
    {
        public Ingredient[,] PizzaIngredients;
        public int MaxSliceSize { get; set; }
        public int MinIngredientNum { get; set; }

        public PizzaParams(int rows, int columns, int maxSliceSize, int minIngredientNum)
        {
            this.MaxSliceSize = maxSliceSize;
            this.MinIngredientNum = minIngredientNum;
            this.PizzaIngredients = new Ingredient[rows, columns];
        }
    }
}
