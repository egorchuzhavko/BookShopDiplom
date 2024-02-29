using BookShop.mvvm.ViewModel;
using BookShop.mvvm.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BookShop.mvvm.Model;

namespace BookShop {
    public partial class App : Application {
        private static ShoppingCartViewModel shoppingCartViewModel;
        public static ShoppingCartViewModel ShoppingCartViewModel {
            get
            {
                if(shoppingCartViewModel == null) {
                    shoppingCartViewModel = new ShoppingCartViewModel();
                }
                return shoppingCartViewModel;
            }
        }

        private static ProfileViewModel profileCartViewModel;
        public static ProfileViewModel ProfileViewModel {
            get
            {
                if (profileCartViewModel == null) {
                    profileCartViewModel = new ProfileViewModel();
                }
                return profileCartViewModel;
            }
        }

        private static CatalogViewModel catalogViewModel;
        public static CatalogViewModel CatalogViewModel {
            get
            {
                if (catalogViewModel == null) {
                    catalogViewModel = new CatalogViewModel();
                }
                return catalogViewModel;
            }
        }

        private static AdminFeedbacksViewModel adminFeedbacksViewModel;
        public static AdminFeedbacksViewModel AdminFeedbacksViewModel {
            get
            {
                if (adminFeedbacksViewModel == null) {
                    adminFeedbacksViewModel = new AdminFeedbacksViewModel();
                }
                return adminFeedbacksViewModel;
            }
            set
            {
                adminFeedbacksViewModel = value;
            }
        }

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new BookShop.MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
