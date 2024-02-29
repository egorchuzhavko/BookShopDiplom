using System;
using System.Collections.Generic;
using System.Text;
using BookShop.mvvm.View;
using BookShop.mvvm.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    public class FeedbackPageViewModel : BaseViewModel {
        Book book;
        public Book BookFeedback {
            get 
            {
                return book; 
            } 
            set
            {
                book = value;
                OnPropertyChanged();
            }
        }
        
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

        string myFeedback;
        public string MyFeedback {
            get
            {
                if (myFeedback == null) {
                    myFeedback = string.Empty;
                }
                return myFeedback;
            }
            set
            {
                myFeedback = value;
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

        public ICommand SendFeedbackCommand => new Command(SendFeedback);

        private void SendFeedback() {
            if ((myFeedback != null & myFeedback != String.Empty) | myFeedback.Length == 0) {
                if (myFeedback.Length > 3) {
                    if (Feedback.SendFeedback(myFeedback, book.Id, App.ProfileViewModel.User.Id)) {
                        Application.Current.MainPage.DisplayAlert("Успех!", "Отзыв был отправлен.", "Ок");
                        Feedbacks.Add(new Feedback {
                            User = App.ProfileViewModel.User,
                            FeedBack = myFeedback
                        });
                        MyFeedback = String.Empty;
                    }
                        
                    else
                        Application.Current.MainPage.DisplayAlert("Ошибка!", "Ошибка отправки уведомления.", "Ок");
                }
                else {
                    Application.Current.MainPage.DisplayAlert("Ошибка!", "Слишком короткий отзыв.", "Ок");
                }
            }
            else {
                Application.Current.MainPage.DisplayAlert("Ошибка!", "Сначала напишите отзыв, чтобы его отправить.", "Ок");
            }
        }
    }
}
