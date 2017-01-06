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
		public void GetIngInSlice_SingleTile()
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBase(pizza);

			Assert.AreEqual(0, solver.GetMushroomsInSlice(new PizzaSlice(0, 0, 0, 0)));
			Assert.AreEqual(1, solver.GetTomatoInSlice(new PizzaSlice(0, 0, 0, 0)));

			Assert.AreEqual(1, solver.GetMushroomsInSlice(new PizzaSlice(1, 1, 1, 1)));
			Assert.AreEqual(0, solver.GetTomatoInSlice(new PizzaSlice(1, 1, 1, 1)));
		}

		[TestMethod]
		public void GetIngInSlice_MultipleTiles_Edges()
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBase(pizza);

			Assert.AreEqual(0, solver.GetMushroomsInSlice(new PizzaSlice(1, 0, 4, 0)));
			Assert.AreEqual(4, solver.GetTomatoInSlice(new PizzaSlice(1, 0, 4, 0)));

			Assert.AreEqual(2, solver.GetMushroomsInSlice(new PizzaSlice(1, 0, 2, 2)));
			Assert.AreEqual(4, solver.GetTomatoInSlice(new PizzaSlice(1, 0, 2, 2)));
		}

		[TestMethod]
		public void GetIngInSlice_MultipleTiles()
		{
			var parser = new Parser();
			var pizza = parser.ParseData(Resources.example);

			var solver = new PizzaSolverBase(pizza);

			Assert.AreEqual(1, solver.GetMushroomsInSlice(new PizzaSlice(3, 1, 4, 2)));
			Assert.AreEqual(3, solver.GetTomatoInSlice(new PizzaSlice(3, 1, 4, 2)));
		}
	}
}
