using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    public class BookOnlyDetailsViewModel : BaseViewModel
    {
        BookOrder selectedBook;
        public BookOrder SelectedBook {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckFeedbackCommand => new Command(CheckFeedbacks);

        void CheckFeedbacks() {
            var vm = new FeedbackPageViewModel { BookFeedback = selectedBook.Book, Feedbacks = Feedback.FindAllFeedbackForBook(selectedBook.Book.Id) };
            var page = new FeedbackPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}
