using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    public class UserHelpViewModel : BaseViewModel
    {
        public ICommand CheckRequestsCommand => new Command(CheckRequests);

        private void CheckRequests() {
            var vm = new UserCheckAllRequestsViewModel { Supportlist=Support.FindUserRequests(App.ProfileViewModel.User.Id) };
            var page = new UserCheckAllRequestsPage { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public ICommand CreateRequestCommand => new Command(CreateRequest);

        private void CreateRequest() {
            var vm = new UserCreateRequestViewModel();
            var page = new UserCreateRequest { BindingContext = vm };
            Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}
