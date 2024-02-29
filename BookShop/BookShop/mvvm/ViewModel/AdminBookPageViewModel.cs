using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    public class AdminBookPageViewModel : BaseViewModel
    {
        public ICommand AddBookOpenPageCommand => new Command(async() => await OpenPageAddBook());

        async Task OpenPageAddBook() {
            await App.Current.MainPage.Navigation.PushModalAsync(new AdminAddBookPage());
        }
    }
}
