using BookShop.mvvm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class AdminFeedbacksViewModel : BaseViewModel {
        ObservableCollection<Feedback> feedbacks;
        public ObservableCollection<Feedback> Feedbacks {
            get
            {
                return feedbacks;
            }
            set
            {
                feedbacks = value;
                OnPropertyChanged();
            }
        }

        Feedback selectedItem;
        public Feedback SelectedItem {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(SelectionCommandFunc);

        private void SelectionCommandFunc() {
            SelectedItem = null;
        }
    }
}
