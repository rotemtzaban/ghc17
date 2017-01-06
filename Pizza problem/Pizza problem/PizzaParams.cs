using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{

    class PizzaParams
    {
        public Engridient[,] PizzaEngridients;
        public int MaxSliceSize { get; set; }
        public int MinIngredientNum { get; set; }

        public PizzaParams(int rows, int columns, int maxSliceSize, int minIngredientNum)
        {
            this.MaxSliceSize = maxSliceSize;
            this.MinIngredientNum = minIngredientNum;
            this.PizzaEngridients = new Engridient[rows, columns];
        }
    }
}
