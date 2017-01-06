using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaTests.Properties;
using Pizza_problem;

namespace PizzaTests
{
	[TestClass]
	public class SolveBaseTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBase(pizza);

			Assert.AreEqual(0, solver.GetMushroomsInSlice(new PizzaSlice(0, 0, 0, 0)));
			Assert.AreEqual(1, solver.GetTomatoInSlice(new PizzaSlice(0, 0, 0, 0)));
		}
	}
}
