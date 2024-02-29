using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminStartPageViewModel : BaseViewModel{
        /*
        public ICommand OpenFeedbacksCommand => new Command(OpenFeedbacks);
        public void OpenFeedbacks() {
            var vm = new AdminFeedbacksViewModel { Feedbacks = Feedback.FindAllFeedbacks() };
            App.AdminFeedbacksViewModel = vm;
            Application.Current.MainPage.Navigation.PushModalAsync(new AdminFeedbacksPage { BindingContext = App.AdminFeedbacksViewModel });
        }*/

        public ICommand OpenFeedbacksCommand {
            get {
                return new Command(async (e) => {
                    var vm = new AdminFeedbacksViewModel { Feedbacks = Feedback.FindAllFeedbacks() };
                    App.AdminFeedbacksViewModel = vm;
                    await Application.Current.MainPage.Navigation.PushModalAsync(new AdminFeedbacksPage { BindingContext = App.AdminFeedbacksViewModel });
                });
            }
        }

        public ICommand OpenAddBookCommand => new Command(OpenAddBook);
        public void OpenAddBook() {
            Application.Current.MainPage.Navigation.PushModalAsync(new AdminAddBookPage());
        }

        public ICommand OpenAllBooksCommand => new Command(OpenAllBooks);
        public void OpenAllBooks() {
            var vm = new AdminAllBooksViewModel { Books = Book.FindAllBooks() };
            var page = new AdminAllBooksPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand OpenOrdersCommand => new Command(OpenOrders);
        public void OpenOrders() {
            var vm = new AdminAllOrdersViewModel { Orders = new System.Collections.ObjectModel.ObservableCollection<Order>(Order.FindAllOrders()) };
            var page = new AdminAllOrdersPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand OpenStatisticsCommand => new Command(OpenStatistics);
        public void OpenStatistics() {
            Application.Current.MainPage.Navigation.PushModalAsync(new AdminStatisticsPage());
        }

        public ICommand SupportListCommand => new Command(OpenSupportList);
        public void OpenSupportList() {
            Application.Current.MainPage.Navigation.PushModalAsync(new AdminAllSupportPage { BindingContext = new AdminAllSupportViewModel { Supportlist = Support.FindAdminRequests() } });
        }

        public ICommand ReportCommand => new Command(Report);
        public void Report() {
            Application.Current.MainPage.Navigation.PushModalAsync(new AdminReportMenuPage { BindingContext = new AdminReportMenuViewModel() });
        }
    }
}
