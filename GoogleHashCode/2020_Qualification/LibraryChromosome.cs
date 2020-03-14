using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace _2020_Qualification
{
    /// <summary>
    /// Traveling Salesman Problem chromosome.
    /// <remarks>
    /// Each gene represents a city index.
    /// </remarks>
    /// </summary>
    [Serializable]
    public class LibraryChromosome : ChromosomeBase
    {
        #region Fields
        private readonly int _mNumberOfLibraries;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryChromosome"/> class.
        /// </summary>
        /// <param name="numberOfLibraries">Number of cities.</param>
        public LibraryChromosome(int numberOfLibraries) : base(numberOfLibraries)
        {
            _mNumberOfLibraries = numberOfLibraries;
            var citiesIndexes = RandomizationProvider.Current.GetUniqueInts(numberOfLibraries, 0, numberOfLibraries);

            for (int i = 0; i < numberOfLibraries; i++)
            {
                ReplaceGene(i, new Gene(citiesIndexes[i]));
            }
        }

        #endregion

        #region implemented abstract members of ChromosomeBase
        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="geneIndex">Gene index.</param>
        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetInt(0, _mNumberOfLibraries));
        }

        /// <summary>
        /// Creates a new chromosome using the same structure of this.
        /// </summary>
        /// <returns>The new chromosome.</returns>
        public override IChromosome CreateNew()
        {
            return new LibraryChromosome(_mNumberOfLibraries);
        }

        /// <summary>
        /// Creates a clone.
        /// </summary>
        /// <returns>The chromosome clone.</returns>
        public override IChromosome Clone()
        {
            var clone = base.Clone() as LibraryChromosome;

            return clone;
        }
        #endregion
    }
}