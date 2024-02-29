using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BookShop.mvvm.ViewModel {
    public class BookDetailsViewModel : BaseViewModel {
        ObservableCollection<Book> books;
        public ObservableCollection<Book> Books {
            get { return books; }
            set
            {
                books = value;
                OnPropertyChanged();
            }
        }

        Book selectedBook;
        public Book SelectedBook {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        int position;
        public int Position {
            get
            {
                if (position != books.IndexOf(selectedBook))
                    return books.IndexOf(selectedBook);
                return position;
            }
            set
            {
                position = value;
                SelectedBook = books[position];
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public ICommand ChangePositionCommand => new Command(ChangePosition);

        void ChangePosition(object obj) {
            string direction = (string)obj;
            if (direction == "L") {
                if (position == 0) {
                    Position = books.Count - 1;
                    return;
                }
                Position -= 1;
            }
            else if (direction == "R") {
                if (position == books.Count - 1) {
                    Position = 0;
                    return;
                }
                Position += 1;
            }
        }

        public ICommand AddBookCommand => new Command(AddBook);

        void AddBook() {
            var allbasket = App.ShoppingCartViewModel.Books;
            List<BookOrder> list = allbasket.Where(x => x.Book.Name == books[position].Name).ToList();
            Console.WriteLine(list.Count);
            if (list.Count == 0) {
                App.ShoppingCartViewModel.Books.Add(new BookOrder { Book = books[position], Count = 1 });
                App.ShoppingCartViewModel.Price = App.ShoppingCartViewModel.Books.Sum(x => x.Book.Price * x.Count);
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Данная книга уже в корзине! Если вы хотите увеличить количество книги в корзине, то можете прожать \"Плюс\" в корзине на интересующей вас книге.", "Ок");
            }
        }

        public ICommand CheckFeedbackCommand => new Command(CheckFeedbacks);

        void CheckFeedbacks() {
            var vm = new FeedbackPageViewModel { BookFeedback = selectedBook, Feedbacks = Feedback.FindAllFeedbackForBook(selectedBook.Id) };
            var page = new FeedbackPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}
