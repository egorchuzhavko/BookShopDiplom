using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    class UserCreateRequestViewModel : BaseViewModel
    {
        string request;
        public string Request {
            get { return request; } set
            {
                request = value;
                Console.WriteLine(request);
                OnPropertyChanged();
            }
        }

        public ICommand CreateRequestCommand => new Command(CreateRequest);
        void CreateRequest() {
            bool result = Support.CreateRequest(App.ProfileViewModel.User.Id, request);
            if (result) Application.Current.MainPage.DisplayAlert("Успех!", "Вы успешно написали вопрос в поддержку!", "Ок");
            else Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка создания вопроса в поддержку!", "Ок");
        }
    }
}
