﻿using HashCodeCommon;
using HashCodeCommon.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        public bool ShouldAnalizeData { get; set; } = false;

        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            var firstRow = reader.GetIntList();
            input.NumberOfBooks = firstRow[0];
            input.NumberOfLibraries = firstRow[1];
            input.NumberOfDays = firstRow[2];

            input.Books = new List<Book>();
            var secondRow = reader.GetIntList();
            for (int i = 0; i < secondRow.Count; i++)
            {
                input.Books.Add(new Book(i) { Score = secondRow[i] });
            }

            input.Libraries = new List<Library>();
            for (int i = 0; i < input.NumberOfLibraries; i++)
            {
                Library library = new Library(i);
                var row1 = reader.GetIntList();
                var row2 = reader.GetIntList();
                library.NumberOfBooks = row1[0];
                library.LibrarySignupTime = row1[1];
                library.BooksPerDay = row1[2];

                library.Books = new SortedSet<Book>(new BooksCompraer());
                foreach (var item in row2)
                {
                    if (!library.Books.Add(input.Books[item]))
                    {
                        int x = 1;
                    }

                    input.Books[item].Libraries.Add(library);
                }

                input.Libraries.Add(library);
            }

            if (ShouldAnalizeData)
                AnalizeData(input);

            return input;
        }

        private void AnalizeData(ProblemInput input)
        {
            Console.WriteLine();

            var distinc = input.Libraries.SelectMany(_ => _.Books).Distinct();
            Console.WriteLine("Distinc score " + distinc.Sum(_ => _.Score));
            Console.WriteLine($"NumberOfBooks: {input.NumberOfBooks}, NumberOfDays:{input.NumberOfDays}, NumberOfLibraries:{input.NumberOfLibraries}");
            Console.WriteLine($"MaxBookScore: { input.Books.Max(_ => _.Score)}, AvgBookScore: { input.Books.Average(_ => _.Score)}, MinBookScore: { input.Books.Min(_ => _.Score)}");
            Console.WriteLine($"LibraryMaxSignupTime: {input.Libraries.Max(_ => _.LibrarySignupTime)}, LibraryAvgSignupTime: {input.Libraries.Average(_ => _.LibrarySignupTime)}, LibraryMinSignupTime: {input.Libraries.Min(_ => _.LibrarySignupTime)}");
            Console.WriteLine($"LibraryMaxBooksPerDay: {input.Libraries.Max(_ => _.BooksPerDay)}, LibraryAvgBooksPerDay: {input.Libraries.Average(_ => _.BooksPerDay)}, LibraryMinBooksPerDay: {input.Libraries.Min(_ => _.BooksPerDay)}");
            Console.WriteLine($"LibraryMaxBooksCount: {input.Libraries.Max(_ => _.Books.Count)}, LibraryAvgBooksCount: {input.Libraries.Average(_ => _.Books.Count)}, LibraryMinBooksCount: {input.Libraries.Min(_ => _.Books.Count)}");
        }
    }

    public class BooksCompraer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            if (x.Score == y.Score)
            {
                return x.Index.CompareTo(y.Index);
            }
            return y.Score.CompareTo(x.Score);
        }
    }
}
