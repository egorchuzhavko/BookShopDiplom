using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminAllOrdersViewModel : BaseViewModel {

        public AdminAllOrdersViewModel() {
            OrderStatuses = GetStatuses();
        }

        ObservableCollection<Model.Order> orders;
        public ObservableCollection<Model.Order> Orders {
            get
            {
                return orders;
            }
            set
            {
                orders = value;
                SearchOrders = orders;
                OnPropertyChanged(nameof(SearchOrders));
                OnPropertyChanged();
            }
        }

        ObservableCollection<Model.Order> searchorders;
        public ObservableCollection<Model.Order> SearchOrders {
            get
            {
                return searchorders;
            }
            set
            {
                searchorders = value;
                OnPropertyChanged();
            }
        }

        Model.Order selectedOrder;
        public Model.Order SelectedOrder {
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

        private OrderStatus selectedStatus;
        public OrderStatus SelectedStatus {
            get
            {
                return selectedStatus;
            }
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }

        List<OrderStatus> orderstatuses;
        public List<OrderStatus> OrderStatuses {
            get
            {
                return orderstatuses;
            }
            set
            {
                orderstatuses = value;
                OnPropertyChanged();
            }
        }

        string idofsearchorder;
        public string Idofsearchorder {
            get
            {
                if(idofsearchorder == null) {
                    idofsearchorder = String.Empty;
                }
                return idofsearchorder;
            }
            set
            {
                idofsearchorder = value;
                FilterById();
                OnPropertyChanged();
            }
        }

        public ICommand OrderSelectionCommand => new Command(GoToOrder);
        void GoToOrder() {
            if (selectedOrder != null) {
                var vm = new AdminOrderInfoViewModel { Order = selectedOrder };
                var page = new AdminOrderInfo { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedOrder = null;
            }
        }

        public ICommand RefreshCommand => new Command(RefreshOrders);

        void RefreshOrders() {
            IsRefreshing = true;
            orders = new ObservableCollection<Model.Order>(Model.Order.FindAllOrders());
            searchorders = orders;
            OnPropertyChanged(nameof(SearchOrders));
            OnPropertyChanged(nameof(Orders));
            SelectedStatus = null;
            IsRefreshing = false;
        }

        public ICommand FilterCommand => new Command(Filter);

        void Filter() {
            if (SelectedStatus != null) {
                SearchOrders = new ObservableCollection<Model.Order>(orders.Where(x => x.Status == SelectedStatus.Value).ToList());
            }
        }

        public void FilterById() {
            if(idofsearchorder==null | idofsearchorder==String.Empty) {
                SearchOrders = orders;
                OnPropertyChanged(nameof(SearchOrders));
            }
            else {
                SearchOrders = new ObservableCollection<Model.Order>(orders.Where(x => Convert.ToString(x.Id).StartsWith(idofsearchorder)));
            }
        }

        public List<OrderStatus> GetStatuses() {
            return new List<OrderStatus> {
                new OrderStatus {Key=1, Value="Отменён"},
                new OrderStatus {Key=2, Value="Оформлен"},
                new OrderStatus {Key=3, Value="Принят"},
                new OrderStatus {Key=4, Value="В пути"},
                new OrderStatus {Key=5, Value="Доставлен"},
                new OrderStatus {Key=6, Value="Завершён"}
            };
        }
    }

}
