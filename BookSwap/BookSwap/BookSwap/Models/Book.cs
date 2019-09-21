using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;

namespace BookSwap.Models
{
    public class Book : ObservableObject
    {
        private string _title;
        private string _author;
        private string _coverImage;
        private string _accentColor;

        public string Title
        {
            get => _title;
            set => SetProperty<string>(ref _title, value);
        }

        public string Author
        {
            get => _author;
            set => SetProperty<string>(ref _author, value);
        }

        public string CoverImage
        {
            get => _coverImage;
            set => SetProperty<string>(ref _coverImage, value);
        }

        public string AccentColor
        {
            get => _accentColor;
            set => SetProperty<string>(ref _accentColor, value);
        }
    }
}
