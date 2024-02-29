using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using MySqlConnector;

namespace BookShop.mvvm.Model
{
    class Support
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public static bool CreateRequest(int userid, string question) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO Поддержка VALUES(0,{userid},'{question}','-')", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static ObservableCollection<Support> FindUserRequests(int userid) {
            ObservableCollection<Support> books = new ObservableCollection<Support>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Поддержка WHERE НомерПользователя={userid}", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    var book = new Support {
                        Id = result.GetInt32("Номер"),
                        IdUser = result.GetInt32("НомерПользователя"),
                        Question = result.GetString("Вопрос"),
                        Answer = result.GetString("Ответ")
                    };
                    books.Add(book);
                }
                con.Close();
                return books;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return books;
            }
        }

        public static ObservableCollection<Support> FindAdminRequests() {
            ObservableCollection<Support> books = new ObservableCollection<Support>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Поддержка WHERE Ответ='-'", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    var book = new Support {
                        Id = result.GetInt32("Номер"),
                        IdUser = result.GetInt32("НомерПользователя"),
                        Question = result.GetString("Вопрос"),
                        Answer = result.GetString("Ответ")
                    };
                    books.Add(book);
                }
                con.Close();
                return books;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return books;
            }
        }

        public static bool UpdateRequestAnswer(int idquestion,string answer) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE Поддержка SET Ответ = '{answer}' WHERE Номер ={idquestion}", con);
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
