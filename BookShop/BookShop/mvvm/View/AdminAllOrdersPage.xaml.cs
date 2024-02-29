using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Plugin.DeviceOrientation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookShop.mvvm.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminAllOrdersPage : ContentPage {
        public AdminAllOrdersPage() {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (SlidePanelAuthorization.Height == 0) {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 10;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.CubicOut;
                SlidePanelAuthorization.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 10;
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
            double startHeight = mainDisplayInfo.Height / 10;
            double endiendHeight = 0;
            uint rate = 16;
            uint length = 300;
            Easing easing = Easing.SinOut;
            SlidePanelAuthorization.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }
    }
}