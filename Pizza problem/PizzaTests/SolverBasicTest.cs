using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaTests.Properties;
using Pizza_problem;

namespace PizzaTests
{
	[TestClass]
	public class SolverBasicTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBasic(pizza);
			var results = solver.Solve();

			Assert.IsNotNull(results);
		}
	}
}
