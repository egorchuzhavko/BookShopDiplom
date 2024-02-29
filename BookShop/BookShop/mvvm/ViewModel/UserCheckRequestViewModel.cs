using BookShop.mvvm.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.mvvm.ViewModel
{
    class UserCheckRequestViewModel : BaseViewModel
    {
        private Support supportRequest;
        public Support SupportRequest {
            get { return supportRequest; }
            set
            {
                supportRequest = value;
                OnPropertyChanged();
            }
        }
    }
}
