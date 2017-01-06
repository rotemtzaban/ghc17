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
		public PizzaParams ParsePath(string path)
		{
			using (var reader = new StreamReader(path))
			{
				return ParseFromStream(reader);
			}
		}

		public PizzaParams ParseData(string data)
		{
			using (var reader = new StringReader(data))
			{
				return ParseFromStream(reader);
			}
		}

	    private static PizzaParams ParseFromStream(TextReader reader)
	    {
		    string first = reader.ReadLine();
		    string[] lineParams = first.Split(' ');
		    PizzaParams pizaParam = new PizzaParams(int.Parse(lineParams[0]), int.Parse(lineParams[1]), int.Parse(lineParams[2]),
			    int.Parse(lineParams[3]));

		    string line = reader.ReadLine();
		    int count = 0;
		    while (line != null)
		    {
			    char[] lineAsChar = line.ToCharArray();
			    for (int i = 0; i < line.Length; i++)
			    {
				    if (lineAsChar[i] == 'M')
				    {
					    pizaParam.PizzaIngredients[i, count] = Ingredient.Mushroom;
				    }
				    else
				    {
					    pizaParam.PizzaIngredients[i, count] = Ingredient.Tomato;
				    }
			    }

			    count++;
			    line = reader.ReadLine();
		    }

		    return pizaParam;
	    }
    }
}
