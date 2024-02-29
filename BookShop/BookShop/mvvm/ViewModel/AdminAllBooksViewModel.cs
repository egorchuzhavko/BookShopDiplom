using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminAllBooksViewModel : BaseViewModel {
        ObservableCollection<Book> books;
        public ObservableCollection<Book> Books {
            get
            {
                return books;
            }
            set
            {
                books = value;
                searchbooks = books;
                OnPropertyChanged(nameof(SearchBooks));
                OnPropertyChanged();
            }
        }

        ObservableCollection<Book> searchbooks;
        public ObservableCollection<Book> SearchBooks {
            get
            {
                return searchbooks;
            }
            set
            {
                searchbooks = value;
                OnPropertyChanged();
            }
        }

        Book selectedBook;

        public Book SelectedBook {
            get { return selectedBook; } set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        string searchtext;
        public string Searchtext {
            get {
                if (searchtext == null)
                    searchtext = String.Empty;
                return searchtext; 
            }
            set
            {
                searchtext = value;
                SearchBooksFunc();
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(DisplayBook);

        private void DisplayBook() {
            if (SelectedBook != null) {
                var vm = new AdminBookDetailsViewModel { Book = selectedBook };
                var bookdetailspage = new AdminBookDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(bookdetailspage);
                SelectedBook = null;
            }
        }

        private void SearchBooksFunc() {
            if(searchtext == null | searchtext == String.Empty) {
                SearchBooks = books;
                OnPropertyChanged(nameof(SearchBooks));
            }
            else {
                SearchBooks = new ObservableCollection<Book>(books.Where(x => x.Name.ToLower().StartsWith(searchtext.ToLower())));
                OnPropertyChanged(nameof(SearchBooks));
            }
        }
    }
}
