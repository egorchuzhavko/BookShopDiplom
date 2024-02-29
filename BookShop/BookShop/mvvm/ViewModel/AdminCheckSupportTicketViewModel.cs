using BookShop.mvvm.Model;
using BookShop.mvvm.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.ViewModel {
    class AdminCheckSupportTicketViewModel : BaseViewModel {
        private Support supportRequest;
        public Support SupportRequest {
            get { return supportRequest; }
            set
            {
                supportRequest = value;
                OnPropertyChanged(nameof(supportRequest));
            }
        }

        string answer;
        public string Answer {
            get { return answer; }
            set
            {
                answer = value;
                OnPropertyChanged();
            }
        }

        public ICommand AnswerRequestCommand => new Command(AnswerRequest);

        private void AnswerRequest() {
            if (answer != "" | answer != "-") {
                bool result = Support.UpdateRequestAnswer(supportRequest.Id, answer);
                if (result) Application.Current.MainPage.DisplayAlert("Успех!", "Вы успешно ответили на вопрос!", "Ок");
                else Application.Current.MainPage.DisplayAlert("Ошибка!", "Была выявлена ошибка!", "Ок");
            }
            else Application.Current.MainPage.DisplayAlert("Ошибка!", "Вам необходимо ввести ответ на вопрос!", "Ок");
        }
    }
}
