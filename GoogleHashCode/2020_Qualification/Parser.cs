using HashCodeCommon;
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

                library.Books = new List<Book>();
                foreach (var item in row2)
                {
                    library.Books.Add(input.Books[item]);
                }

                input.Libraries.Add(library);
            }

            return input;
        }
    }
}
