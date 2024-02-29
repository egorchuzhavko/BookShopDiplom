using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel
{
    class UserCheckAllRequestsViewModel : BaseViewModel {
        public UserCheckAllRequestsViewModel() {
            //Supportlist = new ObservableCollection<Support>(new List<Support> { new Support { Id = 1, IdUser = 1, Answer = "У вирусов нет «желаний». Зараженный организм они используют как фабрику, производящую новые вирусные частицы. Преимущество получают те версии вирусов, которые размножаются интенсивнее и причиняют тем самым больший ущерб организму-хозяину.", Question = "Зачем вирусы убивают своих носителей?" } });
        }

        private ObservableCollection<Support> supportlist;

        public ObservableCollection<Support> Supportlist {
            get
            {
                return supportlist;
            }
            set
            {
                supportlist = value;
                OnPropertyChanged(nameof(Supportlist));
            }
        }

        private Support selectedRequest;
        public Support SelectedRequest {
            get { return selectedRequest; } set
            {
                selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public ICommand RequestSelectionCommand => new Command(OpenRequest);

        private void OpenRequest() {
            if (selectedRequest != null) {
                var vm = new UserCheckRequestViewModel { SupportRequest = selectedRequest };
                var page = new UserCheckRequest { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedRequest = null;
            }
        }
    }
}
