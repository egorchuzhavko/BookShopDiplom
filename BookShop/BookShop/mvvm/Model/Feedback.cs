using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.Model
{
    public class Feedback
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Book BookFb { get; set; }
        public string FeedBack { get; set; }

        public ICommand RemoveFeedBackCommand => new Command(RemoveFeedback);
        void RemoveFeedback() {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"DELETE FROM Отзыв WHERE Отзыв.Номер={this.ID}", con);
                command.ExecuteNonQuery();
                con.Close();
                App.AdminFeedbacksViewModel.Feedbacks.Remove(this);
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
        }

        public static ObservableCollection<Feedback> FindAllFeedbackForBook(int idbook) {
            ObservableCollection<Feedback> feedbacks = new ObservableCollection<Feedback>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Отзыв WHERE Отзыв.НомерКниги={idbook}", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    feedbacks.Add(new Feedback {
                        User = User.FindAllUserInfoById(result.GetInt32("НомерПользователя")),
                        FeedBack = result.GetString("Отзыв")
                    });
                }
                con.Close();
                return feedbacks;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return feedbacks;
            }
        }

        public static ObservableCollection<Feedback> FindAllFeedbacks() {
            ObservableCollection<Feedback> feedbacks = new ObservableCollection<Feedback>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Отзыв ORDER BY Номер DESC", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    feedbacks.Add(new Feedback {
                        ID = result.GetInt32("Номер"),
                        User = User.FindAllUserInfoById(result.GetInt32("НомерПользователя")),
                        BookFb = Book.FindBookReturnClassBook(result.GetInt32("НомерКниги")),
                        FeedBack = result.GetString("Отзыв")
                    });
                }
                con.Close();
                return feedbacks;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return feedbacks;
            }
        }

        public static bool SendFeedback(string feedback, int idbook, int iduser) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO Отзыв VALUES (0,{iduser},{idbook},'{feedback}')", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
