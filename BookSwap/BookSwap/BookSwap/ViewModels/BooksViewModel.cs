﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSwap.Models;
using MvvmHelpers;
using Xamarin.Forms;

namespace BookSwap.ViewModels
{
    public class BooksViewModel : ObservableObject
    {
        private Book _selectedBook;

        public IList<Book> Books { get; set; }
        public Book SwapFromBook { get; set; }
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set { SetProperty<Book>(ref _selectedBook, value); }
        }
        public BooksViewModel()
        {
            SwapFromBook = new Book()
            {
                Title = "Extremely Loud and Incredibly Close",
                Author = "Jonathan Safran Foer",
                Colors = new ColorValues
                {
                    Accent = Color.FromHex("#FFF571"),
                    DarkAccent = Color.FromHex("#F1EE55"),
                    ExtraDarkAccent = Color.FromHex("#F3DD3F"),
                    TitleColor = Color.FromHex("#F00D39"),
                    AccentTextColor = Color.FromHex("#B7A701"),
                },
                UserImage = "https://randomuser.me/api/portraits/women/12.jpg",
             //   Description = "Oskar Schell is a super-smart nine-year old grieving the loss of his father, Thomas, who was killed in the World Trade Center attacks on September 11, 2001. He's feeling depressed and anxious, and feels angry and distant towards his mother.",
                CoverImage = "book_extremelyloud",
            };

            Books = new ObservableCollection<Book>()
            {
                new Book()
                {
                    Title = "Everything is Illuminated",
                    Author = "Jonathan Safran Foer",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/12.jpg",
                    CoverImage = "book_illumated",
                    UserName = "ERNEST ASANOV"
                },
                new Book()
                {
                    Title = "Ulysses",
                    Author = "James Joyce",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/12.jpg",
                    CoverImage = "book_ulyssess",
                    UserName = "KATE BAIKOVA"
                },
                new Book()
                {
                    Title = "Flowers for Algernon",
                    Author = "Daniel Keyes",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/24.jpg",
                    CoverImage = "book_flowers",
                    UserName = "MARINA YALANSKA"
                },
                new Book()
                {
                    Title = "Casino Royale",
                    Author = "Ian Fleming",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/24.jpg",
                    CoverImage = "book_casino",
                    UserName = "VLAD TARAN"
                },
                new Book()
                {
                    Title = "City on the Edge",
                    Author = "Mark Goldman",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/women/32.jpg",
                    CoverImage = "book_city",
                    UserName = "KONST"

                },
                new Book()
                {
                    Title = "The Hobbit",
                    Author = "J. R. R. Tolkien",
                    Colors = ColorPalette.GetNextColorValues(),
                    UserImage = "https://randomuser.me/api/portraits/men/32.jpg",
                    CoverImage = "book_hobbit",
                    UserName = "DENYS BOLDYRIEV"
                }
            };
        }
    }
}
