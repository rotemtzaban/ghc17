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
        static void Main(string[] args)
        {
            Calculator calculator = null;
            Solver solver = new Solver();

            var runner1 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner1.Run(Resources.Example, "Example");

            var runner2 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner2.Run(Resources.b_read_on, "b_read_on");

            var runner3 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner3.Run(Resources.c_incunabula, "c_incunabula");

            var runner4 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner4.Run(Resources.d_tough_choices, "d_tough_choices");

            var runner5 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner5.Run(Resources.e_so_many_books, "e_so_many_books");

            var runner6 = new Runner<ProblemInput, ProblemOutput>("2020_Qualification", new Parser(), solver, new Printer(), calculator);
            runner6.Run(Resources.f_libraries_of_the_world, "f_libraries_of_the_world");


            ZipCreator.CreateCodeZip(@"..\..\..\output\2020_Qualification");

            Console.ReadLine();
        }
    }
}
