using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSwap.Models;
using MvvmHelpers;

namespace BookSwap.ViewModels
{
    public class BooksViewModel : ObservableObject
    {
        public IList<Book> Books { get; set; }

        public BooksViewModel()
        {
            Books = new ObservableRangeCollection<Book>()
            {
                new Book()
                {
                    Title = "Everything is Illuminated",
                    Author = "Jonathan Safran Foer",
                    AccentColor = "#0FF4C3",
                    CoverImage = "book_illumated", 
                },
                new Book()
                {
                Title = "Ulysses",
                Author = "James Joyce",
                AccentColor = "#B76EFE",
                CoverImage = "book_ulyssess",
                },
                new Book()
                {
                    Title = "Flowers for Algernon",
                    Author = "Daniel Keyes",
                    AccentColor = "#FF848b",
                    CoverImage = "book_flowers",
                },
                 new Book()
                {
                    Title = "Everything is Illuminated",
                    Author = "Jonathan Safran Foer",
                    AccentColor = "#0FF4C3",
                    CoverImage = "book_illumated",
                },
                new Book()
                {
                Title = "Ulysses",
                Author = "James Joyce",
                AccentColor = "#B76EFE",
                CoverImage = "book_ulyssess",
                },
                new Book()
                {
                    Title = "Flowers for Algernon",
                    Author = "Daniel Keyes",
                    AccentColor = "#FF848b",
                    CoverImage = "book_flowers",
                },
                 new Book()
                {
                    Title = "Everything is Illuminated",
                    Author = "Jonathan Safran Foer",
                    AccentColor = "#0FF4C3",
                    CoverImage = "book_illumated",
                },
                new Book()
                {
                Title = "Ulysses",
                Author = "James Joyce",
                AccentColor = "#B76EFE",
                CoverImage = "book_ulyssess",
                },
                new Book()
                {
                    Title = "Flowers for Algernon",
                    Author = "Daniel Keyes",
                    AccentColor = "#FF848b",
                    CoverImage = "book_flowers",
                },
            };
        }
    }
}
