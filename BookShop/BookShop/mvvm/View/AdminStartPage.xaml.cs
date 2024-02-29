using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookShop.mvvm.ViewModel;

namespace BookShop.mvvm.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminStartPage : ContentPage {
        public AdminStartPage() {
            InitializeComponent();
            BindingContext = new AdminStartPageViewModel();
        }
    }
}