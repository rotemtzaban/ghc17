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
        private int consoleColorCount = 0;
        private ConsoleColor[] printedColors = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.White,
            ConsoleColor.Cyan
        };

        public void PrintToConsole(PizzaParams pizzaParams, IEnumerable<PizzaSlice> slices)
        {
            CellToPrint[,] pizzaCells = new CellToPrint[pizzaParams.XLength, pizzaParams.YLength];
            for (int y = 0; y < pizzaParams.YLength; y++)
            {
                for (int x = 0; x < pizzaParams.XLength; x++)
                {
                    CellToPrint cell = new CellToPrint();
                    cell.color = ConsoleColor.Green;
                    cell.ingredient = pizzaParams.PizzaIngredients[x, y];
                    pizzaCells[x, y] = cell;
                }
            }

            foreach (PizzaSlice slice in slices)
            {
                ConsoleColor randomColor = GetColor();
                for (int y = slice.TopLeft.Y; y <= slice.BottomRight.Y; y++)
                {
                    for (int x = slice.TopLeft.X; x <= slice.BottomRight.X; x++)
                    {
                        if (pizzaCells[x, y].color != ConsoleColor.Green)
                        {
                            throw new Exception("das for gal");
                        }

                        pizzaCells[x, y].color = randomColor;
                    }
                }
            }

            for (int y = 0; y < pizzaParams.YLength; y++)
            {
                for (int x = 0; x < pizzaParams.XLength; x++)
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

                Console.Write("\n");
            }

			Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintToFile(IEnumerable<PizzaSlice> slices, string path = null)
        {
	        var outputPath = path ?? GetOutputFilePath();
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(slices.Count());

                foreach (var item in slices)
                {
                    writer.WriteLine(item.TopLeft.Y + " " + item.TopLeft.X + " " +
                                     item.BottomRight.Y + " " + item.BottomRight.X);
                }
            }
        }

        private ConsoleColor GetColor()
        {
            int index = consoleColorCount % printedColors.Length;
            consoleColorCount++;
            return printedColors[index];
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
