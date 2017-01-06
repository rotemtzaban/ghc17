using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public class PizzaPrinter
    {
        public void PrintToConsole(PizzaParams pizzaParams, IEnumerable<PizzaSlice> slices)
        {
            CellToPrint[,] pizzaCells = new CellToPrint[pizzaParams.YLength, pizzaParams.XLength];
            for (int rowIndex = 0; rowIndex < pizzaParams.YLength; rowIndex++)
            {
                for (int colIndex = 0; colIndex < pizzaParams.XLength; colIndex++)
                {
                    CellToPrint cell = new CellToPrint();
                    cell.color = ConsoleColor.Black;
                    cell.ingredient = pizzaParams.PizzaIngredients[rowIndex, colIndex];
                    pizzaCells[rowIndex, colIndex] = new CellToPrint();
                }
            }

            foreach (PizzaSlice slice in slices)
            {
                ConsoleColor randomColor = ConsoleColor.Red;

                for (int rowIndex = slice.TopLeft.Y; rowIndex < slice.BottomRight.Y; rowIndex++)
                {
                    for (int colIndex = slice.TopLeft.X; colIndex < slice.BottomRight.X; colIndex++)
                    {
                        if (pizzaCells[rowIndex, colIndex].color != ConsoleColor.Black)
                        {
                            throw new Exception("das for gal");
                        }

                        pizzaCells[rowIndex, colIndex].color = randomColor;
                    }
                }
            }

            for (int rowIndex = 0; rowIndex < pizzaParams.YLength; rowIndex++)
            {
                for (int colIndex = 0; colIndex < pizzaParams.XLength; colIndex++)
                {
                    CellToPrint print = pizzaCells[rowIndex, colIndex];
                    Console.ForegroundColor = print.color;
                    if (print.ingredient == Ingredient.Mushroom)
                    {
                        Console.Write('M');
                    }
                    else
                    {
                        Console.Write('T');
                    }
                }

                Console.Write("/n");
            }
        }

        public void PrintToFile(IEnumerable<PizzaSlice> slices)
        {
            using (StreamWriter writer = new StreamWriter(GetOutputFilePath()))
            {
                writer.WriteLine(slices.Count());

                foreach (var item in slices)
                {
                    writer.WriteLine(item.TopLeft.Y + " " + item.TopLeft.X + " " +
                                     item.BottomRight.Y + " " + item.BottomRight.X);
                }
            }
        }

        private string GetOutputFilePath()
        {
            int index = 1;
            while (File.Exists("result" + index + ".out"))
            {
                index++;
            }

            return "result" + index + ".out";
        }
    }

    public class CellToPrint
    {
        public Ingredient ingredient { get; set; }
        public ConsoleColor color { get; set; }
    }
}
