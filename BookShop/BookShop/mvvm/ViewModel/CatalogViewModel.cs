using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    public class CatalogViewModel : BaseViewModel
    {
        public CatalogViewModel() {
            Statusesbook = GetStatuses();
        }

        ObservableCollection<Book> books;
        public ObservableCollection<Book> Books
        {
            get { return books; }
            set
            {
                books = value;
                OnPropertyChanged();
            }
        }

        ObservableCollection<Book> searchbooks;
        public ObservableCollection<Book> Searchbooks {
            get { return searchbooks; }
            set
            {
                searchbooks = value;
                OnPropertyChanged();
            }
        }

        Book selectedBook;
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        string searchText;
        public string SearchText {
            get { 
                if(searchText==null)
                    searchText = string.Empty;
                return searchText;
            }
            set
            {
                searchText = value;
                SearchBooks();
                OnPropertyChanged();
            }
        }

        bool isRefreshing;

        public bool IsRefreshing {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }

        List<TypebookClass> statusesbook;
        public List<TypebookClass> Statusesbook {
            get
            {
                return statusesbook;
            }
            set
            {
                statusesbook = value;
                OnPropertyChanged();
            }
        }

        int? pricefrom;
        public int? Pricefrom {
            get { return pricefrom; }
            set
            {
                pricefrom = value;
                OnPropertyChanged();
            }
        }

        int? priceto;
        public int? PriceTo {
            get { return priceto; }
            set
            {
                priceto = value;
                OnPropertyChanged();
            }
        }

        TypebookClass selectedstatus;
        public TypebookClass Selectedstatus {
            get
            {
                return selectedstatus;
            }
            set
            {
                selectedstatus = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(DisplayBook);

        private void DisplayBook() {
            if (SelectedBook != null) {
                var vm = new BookDetailsViewModel { Books = books, SelectedBook = selectedBook, Position = books.IndexOf(selectedBook) };
                var bookdetailspage = new BookDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(bookdetailspage);
                SelectedBook = null;
            }
        }

        private void SearchBooks() {
            if (searchText == string.Empty | searchText == "") {
                Searchbooks = books;
            }
            else {
                Searchbooks = new ObservableCollection<Book>(books.Where(x => x.Name.ToLower().StartsWith(searchText.ToLower())));
            }
        }

        public ICommand RefreshCommand => new Command(RefreshBooks);

        void RefreshBooks() {
            IsRefreshing = true;
            var listbooks = new List<Book>();
            Book.FindAllAvailableBooks(out listbooks);
            Books = new ObservableCollection<Book>(listbooks);
            Pricefrom = null;
            PriceTo = null;
            Selectedstatus = null;
            SearchBooks();
            IsRefreshing = false;
        }

        public ICommand FilterCommand => new Command(Filter);

        void Filter() {
            if(pricefrom!=null & pricefrom != 0) {
                Searchbooks = new ObservableCollection<Book>(searchbooks.Where(x => x.Price >= pricefrom).ToList());
            }
            if (priceto != null & priceto != 0) {
                Searchbooks = new ObservableCollection<Book>(searchbooks.Where(x => x.Price <= priceto).ToList());
            }
            if(selectedstatus!= null) {
                Searchbooks = new ObservableCollection<Book>(searchbooks.Where(x => x.Genre == selectedstatus.Value).ToList());
            }
        }

        public List<TypebookClass> GetStatuses() {
            return new List<TypebookClass> {
                new TypebookClass {Key=1, Value="Эпопея"},
                new TypebookClass {Key=2, Value="Эпос"},
                new TypebookClass {Key=3, Value="Роман"},
                new TypebookClass {Key=4, Value="Повесть"},
                new TypebookClass {Key=5, Value="Новелла"},
                new TypebookClass {Key=6, Value="Рассказ"},
                new TypebookClass {Key=7, Value="Cкетч"},
                new TypebookClass {Key=8, Value="Пьеса"},
                new TypebookClass {Key=9, Value="Очерк"},
                new TypebookClass {Key=10, Value="Эссе"},
                new TypebookClass {Key=11, Value="Ода"},
                new TypebookClass {Key=12, Value="Видения"},
                new TypebookClass {Key=13, Value="Баллада"}
            };
        }
    }

    public class TypebookClass {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
