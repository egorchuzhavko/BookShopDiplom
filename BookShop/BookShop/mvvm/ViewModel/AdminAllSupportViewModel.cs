using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    internal class AdminAllSupportViewModel : BaseViewModel {
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
            get { return selectedRequest; }
            set
            {
                selectedRequest = value;
                OnPropertyChanged(nameof(SelectedRequest));
            }
        }

        public ICommand RequestSelectionCommand => new Command(OpenRequest);

        private void OpenRequest() {
            if (selectedRequest != null) {
                var vm = new AdminCheckSupportTicketViewModel { SupportRequest = selectedRequest };
                var page = new AdminCheckSupportTicket { BindingContext = vm };
                Application.Current.MainPage.Navigation.PushModalAsync(page);
                SelectedRequest = null;
            }
        }
    }
}
