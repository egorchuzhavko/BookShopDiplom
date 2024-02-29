using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MySqlConnector;

namespace BookShop.mvvm.Model {
    public class Order {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public float Price { get; set; }
        public ObservableCollection<Book> Books { get; set; }
        public Dictionary<Book, int> BooksInOrder { get; set; }
        public ObservableCollection<BookOrder> BooksOrder { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime OrderUpdateStateDate { get; set; }
        public string Status { get; set; }

        public static List<Order> FindAllOrdersForUser(int idcustomer) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Заказ WHERE НомерПользователя='{idcustomer}'", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new List<Order>();
                while (result.Read()) {
                    var listbooks = new ObservableCollection<BookOrder>(BookOrder.FindBooksInOrder(result.GetInt32("Номер")));

                    temp.Add(new Order {
                        Id = result.GetInt32("Номер"),
                        IdCustomer = idcustomer,
                        Price = result.GetFloat("Цена"),
                        BooksOrder = listbooks,
                        DateOfOrder = result.GetDateTime("ДатаЗаказа"),
                        OrderUpdateStateDate = result.GetDateTime("ДатаИзмененияСтатусаЗаказа"),
                        Status = result.GetString("Статус")
                    });
                }
                con.Close();
                return temp;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<Order> FindAllOrders() {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Заказ ORDER BY Номер DESC", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new List<Order>();
                while (result.Read()) {
                    var listbooks = new ObservableCollection<BookOrder>(BookOrder.FindBooksInOrder(result.GetInt32("Номер")));

                    temp.Add(new Order {
                        Id = result.GetInt32("Номер"),
                        IdCustomer = result.GetInt32("НомерПользователя"),
                        Price = result.GetFloat("Цена"),
                        BooksOrder = listbooks,
                        DateOfOrder = result.GetDateTime("ДатаЗаказа"),
                        OrderUpdateStateDate = result.GetDateTime("ДатаИзмененияСтатусаЗаказа"),
                        Status = result.GetString("Статус")
                    });
                }
                con.Close();
                return temp;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static Order FindLastUserOrder(int userid) {
            var order = new Order();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM `Заказ` WHERE `Заказ`.`НомерПользователя`={userid} ORDER BY `Заказ`.`Номер` DESC LIMIT 1", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    var listbooks = new ObservableCollection<BookOrder>(BookOrder.FindBooksInOrder(result.GetInt32("Номер")));

                    order = new Order {
                        Id = result.GetInt32("Номер"),
                        IdCustomer = result.GetInt32("НомерПользователя"),
                        Price = result.GetFloat("Цена"),
                        BooksOrder = listbooks,
                        DateOfOrder = result.GetDateTime("ДатаЗаказа"),
                        OrderUpdateStateDate = result.GetDateTime("ДатаИзмененияСтатусаЗаказа"),
                        Status = result.GetString("Статус")
                    };
                }
                con.Close();
                return order;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return order;
            }
        }

        public static int MakeOrder(int idcustomer, float price) {
            int res = -1;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                string prc = Convert.ToString(price).Replace(',', '.');
                MySqlCommand command = new MySqlCommand($"INSERT INTO Заказ VALUES (0,'{idcustomer}','{prc}','{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}','{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}','Оформлен');", con);
                command.ExecuteNonQuery();
                con.Close();
                MySqlConnection con1 = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command1 = new MySqlCommand($"SELECT Номер FROM Заказ WHERE НомерПользователя='{idcustomer}' ORDER BY Номер DESC LIMIT 1", con);
                var findid = command1.ExecuteReader();
                while (findid.Read())
                    res = findid.GetInt32("Номер");
                return res;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return res;
            }
        }

        public static bool UpdateOrderStatus(int id, string status) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE Заказ SET Статус='{status}', ДатаИзмененияСтатусаЗаказа='{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}' WHERE Номер={id}", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static int AllOrdersCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"select count(*) from Заказ", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    count = result.GetInt32(0);
                }
                con.Close();
                return count;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return count;
            }
        }

        public static int RejectedOrdersCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"select count(*) from Заказ WHERE Статус='Отменён'", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    count = result.GetInt32(0);
                }
                con.Close();
                return count;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return count;
            }

        }

        public static int ActiveOrdersCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"select count(*) from Заказ WHERE Статус!='Отменён' and Статус!='Оформлен' and Статус!='Завершён'", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    count = result.GetInt32(0);
                }
                con.Close();
                return count;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return count;
            }
        }

        public static List<int> FindIdsOfOrdersInDateTime(DateTime df, DateTime dt) {
            var listofids = new List<int>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT `Заказ`.`Номер` FROM `Заказ` WHERE `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}' and `Заказ`.`Статус`='Завершён'", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    listofids.Add(result.GetInt32(0));
                }
                con.Close();
                return listofids;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return listofids;
            }
        }

        public static double FindSumPriceForOrders(string str) {
            double price = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT SUM(Цена) FROM `Заказ` WHERE `Заказ`.`Номер` in ({str})", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    price = result.GetDouble(0);
                }
                con.Close();
                return price;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return price;
            }
        }
    }
}