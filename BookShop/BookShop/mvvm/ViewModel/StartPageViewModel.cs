using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class StartPageViewModel : BaseViewModel {
        private string emailAvtorize;
        public string EmailAvtorize {
            get
            {
                if (emailAvtorize == null | emailAvtorize == "")
                    emailAvtorize = string.Empty;
                return emailAvtorize;
            }
            set
            {
                emailAvtorize = value;
                OnPropertyChanged();
            }
        }
        private string passwordAvtorize;
        public string PasswordAvtorize {
            get
            {
                if (passwordAvtorize == null | passwordAvtorize == "")
                    passwordAvtorize = string.Empty;
                return passwordAvtorize;
            }
            set
            {
                passwordAvtorize = value;
                OnPropertyChanged();
            }
        }

        private string nameReg;
        public string NameReg {
            get
            {
                if (nameReg == null | nameReg == "")
                    nameReg = string.Empty;
                return nameReg;
            }
            set
            {
                nameReg = value;
                OnPropertyChanged();
            }
        }

        private string surnameReg;
        public string SurnameReg {
            get
            {   
                if (surnameReg == null | surnameReg == "")
                    surnameReg = string.Empty;
                return surnameReg;
            }
            set
            {
                surnameReg = value;
                OnPropertyChanged();
            }
        }

        private string emailReg;
        public string EmailReg {
            get
            {
                if (emailReg == null | emailReg == "")
                    emailReg = string.Empty;
                return emailReg;
            }
            set
            {
                emailReg = value;
                OnPropertyChanged();
            }
        }

        private string passwordReg;
        public string PasswordReg {
            get
            {
                if (passwordReg == null | passwordReg == "")
                    passwordReg = string.Empty;
                return passwordReg;
            }
            set
            {
                passwordReg = value;
                OnPropertyChanged();
            }
        }
        private string repeatpasswordReg;
        public string RepeatPasswordReg {
            get
            {
                if (repeatpasswordReg == null | repeatpasswordReg == "")
                    repeatpasswordReg = string.Empty;
                return repeatpasswordReg;
            }
            set
            {
                repeatpasswordReg = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegistrationCommand => new Command(Registration);

        private void Registration() {
            if (CompareValidationBehavior.IsValidPasswords &
                PasswordValidationBehavior.IsValidPassword & EmailValidatorBehavior.IsValidEmail &
                EmailReg != string.Empty & SurnameReg != string.Empty & NameReg != string.Empty) {
                if (User.CheckForNotExistingEmail(EmailReg)) {
                    if (User.CreateNewUser(NameReg, SurnameReg, EmailReg, MdFive.GetHash(PasswordReg))) {
                        var userdata = new User();
                        var res = User.FindAllUserInfo(emailReg, MdFive.GetHash(passwordReg), out userdata);
                        if (res) {
                            if (userdata.Role == "Покупатель") {
                                App.ProfileViewModel.User = userdata;
                                List<Book> bks = new List<Book>();
                                Book.FindAllAvailableBooks(out bks);
                                App.CatalogViewModel.Books = new ObservableCollection<Book>(bks);
                                Application.Current.MainPage.DisplayAlert("Успех!", "Вы успешно зарегистрировались!", "Ок");
                                Application.Current.MainPage.Navigation.PushAsync(new TabbedPageContainer());
                            }
                        }
                        else {
                            Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка нахождения данных о вашем созданном аккаунте!", "Ок");
                        }
                    }
                    else {
                        Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка создания аккаунта!", "Ок");
                    }
                }
                else {
                    Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка регистрации, пользователь с такой почтой уже существует!", "Ок");
                }
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка регистрации, введите все корректные данные!", "Ок");
            }
        }

        public ICommand AuthorizationCommand => new Command(Authorization);
        private void Authorization() {
            if(EmailAvtorize!=string.Empty & PasswordAvtorize != string.Empty) {
                var userdata = new User();
                var res = User.FindAllUserInfo(emailAvtorize, MdFive.GetHash(passwordAvtorize), out userdata);
                if (res) {
                    if (userdata.Role == "Покупатель") {
                        App.ProfileViewModel.User = userdata;
                        List<Book> bks = new List<Book>();
                        Book.FindAllAvailableBooks(out bks);
                        App.CatalogViewModel.Books = new ObservableCollection<Book>(bks);
                        Application.Current.MainPage.DisplayAlert("Успех!", "Вы успешно вошли!", "Ок");
                        App.Current.MainPage.Navigation.PushAsync(new TabbedPageContainer());
                    }
                    else {
                        if (userdata.Role == "Администратор") {
                            App.Current.MainPage.Navigation.PushAsync(new AdminStartPage());
                        }
                    }
                }
                else {
                    Application.Current.MainPage.DisplayAlert("Ошибка!", "Вы ввели неверные данные!", "Ок");
                }
            }
        }
    }
}
