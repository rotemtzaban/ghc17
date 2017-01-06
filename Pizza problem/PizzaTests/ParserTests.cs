using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizza_problem;
using Pizza_problem.Properties;

namespace PizzaTests
{
	[TestClass]
	public class ParserTests
	{
		[TestMethod]
		public void CheckParser()
		{
            Parser parser = new Parser();
            PizzaParams pizaParams = parser.ParseData(Resources.example);

            Assert.AreEqual(pizaParams.MaxSliceSize, 6);
            Assert.AreEqual(pizaParams.MinIngredientNum, 1);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[3, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[4, 0], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[0, 1], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[1, 1], Ingredient.Mushroom);
			Assert.AreEqual(pizaParams.PizzaIngredients[2, 1], Ingredient.Mushroom);
			Assert.AreEqual(pizaParams.PizzaIngredients[3, 1], Ingredient.Mushroom);
			Assert.AreEqual(pizaParams.PizzaIngredients[4, 1], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[0, 2], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[1, 2], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[2, 2], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[3, 2], Ingredient.Tomato);
			Assert.AreEqual(pizaParams.PizzaIngredients[4, 2], Ingredient.Tomato);
        }
	}
}
