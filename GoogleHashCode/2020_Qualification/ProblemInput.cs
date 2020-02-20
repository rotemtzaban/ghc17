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
        private bool isUpdated = false;
        private long score = 0;
        public Library(int index) : base(index)
        {
        }

        public void SendBookToScan(Book bookToScan)
        {
            isUpdated = false;
            Books.Remove(bookToScan);
            SelectedBooks.Add(bookToScan);
            GivenScore += bookToScan.Score;
        }

        public void SendBooksToScan(List<Book> booksToScan)
        {
            isUpdated = false;
            foreach (var book in booksToScan)
            {
                Books.Remove(book);
                SelectedBooks.Add(book);
                GivenScore += book.Score;
            }
        }

        public long LibraryScore(int numberOfDays)
        {
            if (isUpdated)
            {
                return score;
            }

            score = Books.Take(Math.Max(BooksPerDay * numberOfDays, Books.Count)).Sum(_ => _.Score);
            isUpdated = true;
            return score;
        }

        public int GivenScore { get; set; } = 0;
        public int NumberOfBooks { get; set; }
        public SortedSet<Book> Books { get; set; }
        public List<Book> SelectedBooks { get; set; } = new List<Book>();
        public int BooksPerDay { get; set; }
        public int LibrarySignupTime { get; set; }
        public int LibaryStartSignUpTime { get; set; }
    }
}
