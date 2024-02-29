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

namespace BookShop.mvvm.ViewModel {
    public class ProfileViewModel : BaseViewModel {

        User user;
        public User User {
            get
            {
                return user;
            }
            set
            {
                user = value;
                orders = new ObservableCollection<Order>(Order.FindAllOrdersForUser(user.Id).Where(x => x.Status != "Завершён" & x.Status!="Отменён"));
                var listofbookincatalog = new List<Book>();
                var res = Book.FindAllAvailableBooks(out listofbookincatalog);
                Debug.WriteLine(res);
                App.CatalogViewModel.Books = new ObservableCollection<Book>(listofbookincatalog);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Orders));
            }
        }

        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        Order selectedOrder;
        public Order SelectedOrder {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
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

        public ICommand CheckProfileCommand => new Command(GoToProfile);

        private void GoToProfile() {
            var vm = new ProfileDeepInfoViewModel { User = User };
            var page = new ProfileDeepInfoPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand HelpCommand => new Command(GoToHelp);

        private void GoToHelp() {
            var vm = new UserHelpViewModel();
            var page = new UserHelpPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand CheckAllOrdersCommand => new Command(GoToAllOrders);

        private void GoToAllOrders() {
            var vm = new AllOrdersViewModel { Orders = new ObservableCollection<Order>(Order.FindAllOrdersForUser(user.Id)) };
            var page = new AllOrdersPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand OrderSelectionCommand => new Command(GoToOrder);
        void GoToOrder() {
            if (selectedOrder != null) {
                var vm = new OrderDetailsViewModel { Order = selectedOrder };
                var page = new OrderDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedOrder = null;
            }
        }

        public ICommand SpravkaCommand => new Command(Spravka);
        void Spravka() {
            App.Current.MainPage.DisplayAlert("Справка", "Данное приложение предназначено для заказа книг.\nВ разделе каталог показаны книги, где перейдя на них можно посмотреть информацию о них.\n\nВ разделе Корзина можно посмотреть товары в корзине и выбрать их количество. Можно оформить заказ и увидеть сумму заказа.\n\nВ разделе профиль можно посмотреть информацию о заказах.", "Ок");
        }

        public ICommand RefreshCommand => new Command(RefreshOrders);

        void RefreshOrders() {
            IsRefreshing = true;
            orders = new ObservableCollection<Order>(Order.FindAllOrdersForUser(user.Id).Where(x => x.Status != "Завершён" & x.Status != "Отменён"));
            OnPropertyChanged(nameof(Orders));
            IsRefreshing = false;
        }
    }
}
