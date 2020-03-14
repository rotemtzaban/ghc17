using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using HashCodeCommon;

namespace _2020_Qualification
{
    class SolverGenetic : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            IPopulation population = new Population(100, 200, new LibraryChromosome(input.NumberOfLibraries));

            var fitness = new FitnessCalculator(input);
            ISelection selection = new EliteSelection();
            ICrossover crossover = new OrderedCrossover();
            IMutation mutation = new PartialShuffleMutation();
            GeneticAlgorithm ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.Termination = new OrTermination(
                new TimeEvolvingTermination(TimeSpan.FromMinutes(1)), 
                new FitnessStagnationTermination(500));
            ga.GenerationRan += (sender, args) =>
            {
                var chromosome = ga.BestChromosome;
                var d = fitness.Evaluate(chromosome);
                Console.WriteLine(d);
            };
            ga.Start();
            var bestChromosome = ga.BestChromosome;

            var output = fitness.CreateProblemOutput(bestChromosome.GetGenes());
            return output;
        }
    }

    internal class FitnessCalculator : IFitness
    {
        private readonly ProblemInput _input;

        public FitnessCalculator(ProblemInput input)
        {
            _input = input;
        }

        public double Evaluate(IChromosome chromosome)
        {
            LibraryChromosome libraryChromosome = (LibraryChromosome) chromosome;

            Gene[] genes = libraryChromosome.GetGenes();

            var calculator = new Calculator();
            var output = CreateProblemOutput(genes);

            return calculator.Calculate(_input, output);
        }

        public ProblemOutput CreateProblemOutput(Gene[] genes)
        {
            HashSet<Book> selectedBooks = new HashSet<Book>();
            ProblemOutput output = new ProblemOutput { libaries = new List<Library>() };

            int currentTime = 0;
            foreach (var gene in genes)
            {
                if (currentTime >= _input.NumberOfDays) break;


                var library = GetLibraryScore((int)gene.Value, _input, currentTime, selectedBooks);

                output.libaries.Add(library);

                currentTime += library.LibrarySignupTime;
            }

            return output;
        }
        private static Library GetLibraryScore(int libraryIndex, ProblemInput input, 
            int currentTime, HashSet<Book> selectedBooks)
        {
            var library = input.Libraries[libraryIndex];

            long counter = 0;
            List<Book> takenBooks = new List<Book>();
            var availableTime = Math.BigMul(input.NumberOfDays - currentTime - library.LibrarySignupTime, library.BooksPerDay);

            foreach (var libraryBook in library.Books)
            {
                if(selectedBooks.Contains(libraryBook)) continue;
                if (counter++ >= availableTime) break;
                
                takenBooks.Add(libraryBook);
                selectedBooks.Add(libraryBook);
            }

            return new Library(library.Index)
            {
                LibrarySignupTime = library.LibrarySignupTime,
                BooksPerDay = library.BooksPerDay,
                LibaryStartSignUpTime = currentTime, 
                Books = library.Books,
                SelectedBooks = takenBooks
            };
        }
    }
}
