using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
    public enum Engridient
    {
        Tomato,
        Mushroom
    }

    class Parser
    {
        public void Parse()
        {
            using (StreamReader reader = new StreamReader("small.in"))
            {
                string first = reader.ReadLine();
                string[] lineParams = first.Split(' ');
                PizzaParams pizaParam = new PizzaParams(int.Parse(lineParams[0]), int.Parse(lineParams[1]), int.Parse(lineParams[2]), int.Parse(lineParams[3]));

                string line = reader.ReadLine();
                while (line != null)
                {
                    char[] lineAsChar = line.ToCharArray();
                    Engridient[] eng = new Engridient[line.Length];
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (lineAsChar[i] == 'M')
                        {
                            eng[i] = Engridient.Mushroom;
                        }
                        else
                        {
                            eng[i] = Engridient.Tomato;
                        }
                    }
                }
            }
        }
    }
}
