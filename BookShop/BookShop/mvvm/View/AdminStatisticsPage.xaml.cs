using BookShop.mvvm.Model;
using BookShop.mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminStatisticsPage : ContentPage {
        public AdminStatisticsPage() {
            InitializeComponent();
            BindingContext = new AdminStatisticsViewModel { UsersCount = User.UsersCount(), Activeorderscount = Order.ActiveOrdersCount(), Allorderscount = Order.AllOrdersCount(), Rejectedorderscount = Order.RejectedOrdersCount(), Remainingbookscount = Book.RemainingBooksCount(), Soldbookscount = BookOrder.SoldBooksCount() };
        }
    }
}