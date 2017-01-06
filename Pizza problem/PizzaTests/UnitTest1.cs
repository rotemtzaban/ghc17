using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizza_problem;

namespace PizzaTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void CheckParser()
		{
            Parser parser = new Parser();
            PizzaParams pizaParams = parser.Parse(@"C:\Shachar git\ghc17\Pizza problem\Pizza problem\inputs\example.in");

            Assert.AreEqual(pizaParams.MaxSliceSize, 6);
            Assert.AreEqual(pizaParams.MinIngredientNum, 1);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 1], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 2], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 3], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[0, 4], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 1], Ingredient.Mushroom);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 2], Ingredient.Mushroom);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 3], Ingredient.Mushroom);
            Assert.AreEqual(pizaParams.PizzaIngredients[1, 4], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 0], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 1], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 2], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 3], Ingredient.Tomato);
            Assert.AreEqual(pizaParams.PizzaIngredients[2, 4], Ingredient.Tomato);
        }
	}
}
