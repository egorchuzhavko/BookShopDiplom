using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookShop.mvvm.Model {
    public class BookOrder { 
        public Book Book { get; set; }
        public int Count { get; set; }

        public static bool MakeBusyBooksByOrder(ObservableCollection<BookOrder> bks, int orderid) {
            bool flag = false;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                foreach(var book in bks) {
                    MySqlConnection con = new MySqlConnection(connStr);
                    con.Open();
                    MySqlCommand command = new MySqlCommand($"INSERT INTO ЗаказКнига VALUES ({orderid},{book.Book.Id},{book.Count},0)", con);
                    command.ExecuteNonQuery();
                    con.Close();
                    MySqlConnection con1 = new MySqlConnection(connStr);
                    con1.Open();
                    MySqlCommand command1 = new MySqlCommand($"UPDATE Книга SET Количество=Количество-{book.Count} WHERE Книга.Номер={book.Book.Id}", con1);
                    command1.ExecuteNonQuery();
                    con1.Close();
                }
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return flag;
            }
        }

        //SELECT `Книга`.* FROM `ЗаказКнига` JOIN `Книга` on `Книга`.`Номер`=`ЗаказКнига`.`НомерКниги` WHERE `ЗаказКнига`.`НомерЗаказа`=1

        public static ObservableCollection<BookOrder> FindBooksInOrder(int idoforder) {
            ObservableCollection<BookOrder> bks = new ObservableCollection<BookOrder>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT `Книга`.*, `ЗаказКнига`.`Количество` as Колво FROM `ЗаказКнига` JOIN `Книга` on `Книга`.`Номер`=`ЗаказКнига`.`НомерКниги` WHERE `ЗаказКнига`.`НомерЗаказа`='{idoforder}'", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    bks.Add( new BookOrder {
                        Book = new Book {
                            Id = result.GetInt32("Номер"),
                            Name = result.GetString("Название"),
                            Price = result.GetFloat("Цена"),
                            Description = result.GetString("Описание"),
                            Genre = result.GetString("Жанр"),
                            Author = result.GetString("Автор"),
                            Image = result.GetString("Фото"),
                        },
                        Count = result.GetInt32("Колво")
                    });
                }
                con.Close();
                return bks;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return bks;
            }
        }

        public static int SoldBooksCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"select SUM(`ЗаказКнига`.`Количество`) FROM `ЗаказКнига` JOIN `Заказ` ON `Заказ`.`Номер`=`ЗаказКнига`.`НомерЗаказа` WHERE `Заказ`.`Статус`!='Отменён'", con);
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

        public static int FindSumCountBooksSelled(string str) {
            var price = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT sum(Количество) FROM `ЗаказКнига` WHERE `ЗаказКнига`.`НомерКниги` in ({str})", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    price = result.GetInt32(0);
                }
                con.Close();
                return price;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return price;
            }
        }


        public ICommand RemoveBookCommand => new Command(RemoveBookFromBooks);
        void RemoveBookFromBooks() {
            App.ShoppingCartViewModel.Books.Remove(this);
            App.ShoppingCartViewModel.Price = App.ShoppingCartViewModel.Books.Sum(x => x.Book.Price * x.Count);
        }

        public ICommand ChangeCountCommand => new Command(ChangeCount);
        void ChangeCount(object obj) {
            string type = obj as string;
            if (type == "Minus") {
                if (App.ShoppingCartViewModel.Books[App.ShoppingCartViewModel.Books.IndexOf(this)].Count == 1) {
                    App.ShoppingCartViewModel.Books.Remove(this);
                }
                else {
                    App.ShoppingCartViewModel.Books[App.ShoppingCartViewModel.Books.IndexOf(this)].Count--;
                }
            }
            else {
                App.ShoppingCartViewModel.Books[App.ShoppingCartViewModel.Books.IndexOf(this)].Count++;
            }
            List<BookOrder> bksnew = new List<BookOrder>(App.ShoppingCartViewModel.Books);
            App.ShoppingCartViewModel.Books = new System.Collections.ObjectModel.ObservableCollection<BookOrder>(bksnew);
        }
    }
}
