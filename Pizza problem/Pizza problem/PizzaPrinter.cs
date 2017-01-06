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
            for (int y = 0; y < pizzaParams.YLength; y++)
            {
                for (int x = 0; x < pizzaParams.XLength; x++)
                {
                    CellToPrint cell = new CellToPrint();
                    cell.color = ConsoleColor.Black;
                    cell.ingredient = pizzaParams.PizzaIngredients[x, y];
                    pizzaCells[x, y] = new CellToPrint();
                }
            }

            foreach (PizzaSlice slice in slices)
            {
                ConsoleColor randomColor = ConsoleColor.Red;

                for (int x = slice.TopLeft.Y; x < slice.BottomRight.Y; x++)
                {
                    for (int y = slice.TopLeft.X; y < slice.BottomRight.X; y++)
                    {
                        if (pizzaCells[x, y].color != ConsoleColor.Black)
                        {
                            throw new Exception("das for gal");
                        }

                        pizzaCells[x, y].color = randomColor;
                    }
                }
            }

            for (int x = 0; x < pizzaParams.YLength; x++)
            {
                for (int y = 0; y < pizzaParams.XLength; y++)
                {
                    CellToPrint print = pizzaCells[x, y];
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
