using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_Qualification
{
    public class ProblemInput
    {
        public int NumberOfBooks { get; set; }
        public int NumberOfLibraries { get; set; }
        public int NumberOfDays { get; set; }
        public List<Book> Books { get; set; }
        public List<Library> Libraries { get; set; }
    }

    public class Book : IndexedObject
    {
        public Book(int index) : base(index)
        {
        }

        public int Score { get; set; }
    }

    public class Library : IndexedObject
    {
        public Library(int index) : base(index)
        {
        }

        public int NumberOfBooks { get; set; }
        public List<Book> Books { get; set; }
        public List<Book> SelectedBooks { get; set; }
        public int BooksPerDay { get; set; }
        public int LibrarySignupTime { get; set; }
        public int LibaryStartSignUpTime { get; set; }
    }
}
