using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class OrderDetailsViewModel : BaseViewModel {
        Order order;
        public Order Order {
            get
            {
                return order;
            }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        BookOrder selectedBook;
        public BookOrder SelectedBook {
            get
            {
                return selectedBook;
            }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckBookDetailsCommand => new Command(GoToDetails);

        private void GoToDetails() {
            if (SelectedBook != null) {
                var vm = new BookOnlyDetailsViewModel { SelectedBook = selectedBook };
                var page = new BookOnlyDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedBook = null;
            }
        }
    }
}
