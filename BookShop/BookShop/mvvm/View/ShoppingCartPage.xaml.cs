using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.DeviceOrientation;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.ViewModel {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class ShoppingCartPage : ContentPage {
        public ShoppingCartPage() {
            InitializeComponent();
            BindingContext = App.ShoppingCartViewModel;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => SlidePanelOrder.HeightRequest = input;
            double startHeight = mainDisplayInfo.Height / 10;
            double endiendHeight = 0;
            uint rate = 16;
            uint length = 300;
            Easing easing = Easing.SinOut;
            SlidePanelOrder.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }

        private void Button_Clicked(object sender, EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (SlidePanelOrder.Height == 0) {
                Action<double> callback = input => SlidePanelOrder.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 10;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.CubicOut;
                SlidePanelOrder.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else {
                Action<double> callback = input => SlidePanelOrder.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 10;
                double endiendHeight = 0;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.SinOut;
                SlidePanelOrder.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);

            }
        }
    }
}