using BookShop.mvvm.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    internal class ProfileDeepInfoViewModel : BaseViewModel {

        User user;
        public User User {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeInfoCommand => new Command(ChangeInfo);

        void ChangeInfo() {
            if (user.UpdateInfoUser()) {
                Application.Current.MainPage.DisplayAlert("Успех!", "Вы успешно обновили информацию о себе!", "Ок");
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка обновления информации о себе!", "Ок");
            }
        }

    }
}
