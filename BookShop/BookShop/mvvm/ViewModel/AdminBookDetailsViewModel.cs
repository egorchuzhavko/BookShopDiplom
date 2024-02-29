using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminBookDetailsViewModel : BaseViewModel {
        Book book;
        public Book Book {
            get
            {
                return book;
            }
            set
            {
                book = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckFeedbackCommand => new Command(CheckFeedbacks);

        void CheckFeedbacks() {
            var vm = new FeedbackPageViewModel { BookFeedback = Book, Feedbacks = Feedback.FindAllFeedbackForBook(Book.Id) };
            var page = new FeedbackPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand UpdateBookCountCommand => new Command(UpdateBookCount);

        void UpdateBookCount() {
            if (book.UpdateCountBooks()) {
                Application.Current.MainPage.DisplayAlert("Успех!", "Изменения сохранены!", "Ок");
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Изменения не были сохранены!", "Ок");
            }
        }
    }
}
