using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHelpPage : ContentPage {
        public UserHelpPage() {
            InitializeComponent();
            BindingContext = new ViewModel.UserHelpViewModel();
        }
    }
}