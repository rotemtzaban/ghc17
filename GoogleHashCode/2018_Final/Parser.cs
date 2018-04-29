using System;
using System.IO;
using HashCodeCommon;
using HashCodeCommon.HelperClasses;

namespace _2018_Final
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            int[] firstLine = ReadLineAsIntArray(reader);
            ProblemInput input = new ProblemInput
            {
                Columns = firstLine[1],
                Rows = firstLine[0],
                MaxDistance = firstLine[2],
                BuildingProjects = new BuildingProject[firstLine[3]]
            };

            for (int i = 0; i < input.BuildingProjects.Length; i++)
            {
                string[] buildingLine = reader.GetStringList();
                BuildingType type = buildingLine[0] == "R" ? BuildingType.Residential : BuildingType.Utility;
                int rows = int.Parse(buildingLine[1]);
                int columns = int.Parse(buildingLine[2]);
                int number = int.Parse(buildingLine[3]);

                BuildingProject project = new BuildingProject(i)
                {
                    BuildingType = type,
                    Capacity = number,
                    UtilityType = number,
                    Plan = new bool[rows, columns]
                };

                for (int j = 0; j < rows; j++)
                {
                    var row = reader.ReadLine();
                    for (int k = 0; k < columns; k++)
                    {
                        project.Plan[j, k] = row[k] == '#';
                    }
                }

                input.BuildingProjects[i] = project;
            }

            return input;
        }
    }
}