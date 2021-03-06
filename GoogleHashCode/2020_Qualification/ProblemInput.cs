﻿using HashCodeCommon;
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

        public void BookTakenToScan()
        {
            foreach (var library in Libraries)
            {
                library.BookTakenFromOhterLibrary(this);
            }
        }

        public int Score { get; set; }
        public HashSet<Library> Libraries { get; set; } = new HashSet<Library>();
    }

    public class Library : IndexedObject
    {
        private bool isUpdated = false;
        private long score = 0;
        public Library(int index) : base(index)
        {
        }

        public void BookTakenFromOhterLibrary(Book book)
        {
            Books.Remove(book);
        }

        public void SendBookToScan(Book bookToScan)
        {
            isUpdated = false;
            Books.Remove(bookToScan);
            bookToScan.Libraries.Remove(this);
            SelectedBooks.Add(bookToScan);
            GivenScore += bookToScan.Score;
            bookToScan.BookTakenToScan();
        }

        public void SendBooksToScan(List<Book> booksToScan)
        {
            isUpdated = false;
            foreach (var book in booksToScan)
            {
                Books.Remove(book);
                book.Libraries.Remove(this);
                SelectedBooks.Add(book);
                GivenScore += book.Score;

                book.BookTakenToScan();
            }
        }

        public long LibraryScore(int numberOfDays)
        {
            if (isUpdated)
            {
                return score;
            }

            int maxToTake = Math.Max(BooksPerDay * numberOfDays, Books.Count);
            int index = 0;
            long currentScore = 0;
            foreach (var book in Books)
            {
                currentScore += book.Score;
                index++; 
                if (index >= maxToTake)
                {
                    break;
                }
            }

            score = currentScore;
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
