using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminOrderInfoViewModel : BaseViewModel {

        public AdminOrderInfoViewModel() {
            OrderStatuses = GetStatuses();
        }

        Order order;
        public Order Order {
            get
            {
                return order;
            }
            set
            {
                order = value;
                SelectedStatus = OrderStatuses.Where(x => x.Value == order.Status).ToList()[0];
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
                if (selectedStatus.Value != order.Status) {
                    Order.Status = selectedStatus.Value;
                    Order.OrderUpdateStateDate = DateTime.Now;
                    Order.UpdateOrderStatus(order.Id, selectedStatus.Value);
                    OnPropertyChanged(nameof(Order));
                }
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

    public class OrderStatus {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
