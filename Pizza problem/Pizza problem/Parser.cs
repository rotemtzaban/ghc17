using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public enum Ingredient
    {
        Tomato,
        Mushroom
    }

    public class Parser
    {
        public PizzaParams Parse(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string first = reader.ReadLine();
                string[] lineParams = first.Split(' ');
                PizzaParams pizaParam = new PizzaParams(int.Parse(lineParams[0]), int.Parse(lineParams[1]), int.Parse(lineParams[2]), int.Parse(lineParams[3]));

                string line = reader.ReadLine();
                int count = 0;
                while (line != null)
                {
                    char[] lineAsChar = line.ToCharArray();
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (lineAsChar[i] == 'M')
                        {
                            pizaParam.PizzaIngredients[count, i] = Ingredient.Mushroom;
                        }
                        else
                        {
                            pizaParam.PizzaIngredients[count, i] = Ingredient.Tomato;
                        }
                    }

                    count++;
                    line = reader.ReadLine();
                }

                return pizaParam;
            }
        }
    }
}
