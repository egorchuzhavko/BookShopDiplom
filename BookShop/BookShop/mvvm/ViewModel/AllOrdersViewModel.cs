using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AllOrdersViewModel : BaseViewModel {
        ObservableCollection<Order> orders;
        public ObservableCollection<Order> Orders {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                OnPropertyChanged();
            }
        }

        Order selectedOrder;
        public Order SelectedOrder {
            get
            {
                return selectedOrder;
            }
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

        public ICommand OrderSelectionCommand => new Command(GoToOrder);
        void GoToOrder() {
            if (selectedOrder != null) {
                var vm = new OrderDetailsViewModel { Order = selectedOrder };
                var page = new OrderDetailsPage { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedOrder = null;
            }
        }

        public ICommand RefreshCommand => new Command(RefreshOrders);

        void RefreshOrders() {
            IsRefreshing = true;
            orders = new ObservableCollection<Order>(Order.FindAllOrdersForUser(App.ProfileViewModel.User.Id));
            OnPropertyChanged(nameof(Orders));
            IsRefreshing = false;
        }
    }
}
