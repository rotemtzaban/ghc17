using _2020_Qualification.Properties;
using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    class Program
    {
        private const int NUM_OF_ATTEMPTS = 10;

        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            var solver = new SolverGenetic();

            // AnalizeData();

            List<double> runParams = new List<double>();
            for (int i = 0; i < NUM_OF_ATTEMPTS; i++)
            {
                runParams.Add(0.5 + i / (double)NUM_OF_ATTEMPTS);
            }

            var runner1 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            //runner1.Run(Resources.Example, "a_Example");

            ZipCreator.CreateCodeZip(@"..\..\..\output\2020_Qualification");

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner2.Run(Resources.b_read_on, "b_read_on");

            var runner3 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            // runner3.Run(Resources.c_incunabula, "c_incunabula", NUM_OF_ATTEMPTS, true, runParams);

            var runner4 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            // runner4.Run(Resources.d_tough_choices, "d_tough_choices", NUM_OF_ATTEMPTS, true, runParams);

            var runner5 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            //runner5.Run(Resources.e_so_many_books, "e_so_many_books", NUM_OF_ATTEMPTS, true, runParams);

            var runner6 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            //runner6.Run(Resources.f_libraries_of_the_world, "f_libraries_of_the_world", NUM_OF_ATTEMPTS, true, runParams);

            Console.ReadLine();
        }

        private static void AnalizeData()
        {

            Parser parser = new Parser();
            parser.ShouldAnalizeData = true;
            parser.ParseFromData(Resources.Example);
            parser.ParseFromData(Resources.b_read_on);
            parser.ParseFromData(Resources.c_incunabula);
            parser.ParseFromData(Resources.d_tough_choices);
            parser.ParseFromData(Resources.e_so_many_books);
            parser.ParseFromData(Resources.f_libraries_of_the_world);

            Console.Read();
        }
    }
}
