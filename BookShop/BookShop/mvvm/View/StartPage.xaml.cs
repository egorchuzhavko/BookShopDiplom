using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.DeviceOrientation;

namespace BookShop {
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }
        //Для слайдера авторизации
        private void Button_Clicked(object sender, EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (SlidePanelAuthorization.Height == 0) {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 8;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.CubicOut;
                SlidePanelAuthorization.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else {
                Action<double> callback = input => SlidePanelAuthorization.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 8;
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
            double startHeight = mainDisplayInfo.Height / 8;
            double endiendHeight = 0;
            uint rate = 16;
            uint length = 300;
            Easing easing = Easing.SinOut;
            SlidePanelAuthorization.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }

        //Для слайдера регистрации
        private void Button_Clicked1(object sender, EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;


            if (SlidePanelRegistration.Height == 0) {
                Action<double> callback = input => SlidePanelRegistration.HeightRequest = input;
                double startHeight = 0;
                double endHeight = mainDisplayInfo.Height / 4;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.CubicOut;
                SlidePanelRegistration.Animate("anim", callback, startHeight, endHeight, rate, length, easing);
            }
            else {
                Action<double> callback = input => SlidePanelRegistration.HeightRequest = input;
                double startHeight = mainDisplayInfo.Height / 4;
                double endiendHeight = 0;
                uint rate = 16;
                uint length = 300;
                Easing easing = Easing.SinOut;
                SlidePanelRegistration.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);

            }
        }

        void TapGestureRecognizer_Tapped1(System.Object sender, System.EventArgs e) {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            Action<double> callback = input => SlidePanelRegistration.HeightRequest = input;
            double startHeight = mainDisplayInfo.Height / 4;
            double endiendHeight = 0;
            uint rate = 16;
            uint length = 300;
            Easing easing = Easing.SinOut;
            SlidePanelRegistration.Animate("anim", callback, startHeight, endiendHeight, rate, length, easing);
        }
    }
}
