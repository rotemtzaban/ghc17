using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_problem.Properties;

namespace Pizza_problem
{
    class Program
    {
        static void Main(string[] args)
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBasic(pizza);
			var results = solver.Solve();

	        var printer = new PizzaPrinter();
			printer.PrintToFile(results);
			printer.PrintToConsole(pizza, results);

            Console.ReadKey();
		}
    }
}
