using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Plugin.DeviceOrientation;

namespace BookShop.mvvm.ViewModel {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogPage : ContentPage {
        public CatalogPage() {
            InitializeComponent();
            BindingContext = App.CatalogViewModel;
        }


        private void Button_Clicked(object sender, EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (SlidePanelAuthorization.Height == 0) {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 7;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.CubicOut;
                SlidePanelAuthorization.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 7;
                double endiendHeight = 0;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.SinOut;
                SlidePanelAuthorization.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);

            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
            double startHeight = mainDisplayInfo.Height / 7;
            double endiendHeight = 0;
            uint rate = 16;
            uint length = 300;
            Easing easing = Easing.SinOut;
            SlidePanelAuthorization.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }
    }
}