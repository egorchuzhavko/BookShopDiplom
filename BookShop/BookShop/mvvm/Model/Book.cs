using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MySqlConnector;
using Xamarin.Essentials;
using System.Collections.ObjectModel;

namespace BookShop.mvvm.Model {
    public class Book {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public int CountBooks { get; set; }

        public ICommand RemoveBookCommand => new Command(RemoveBookFromBooks);
        void RemoveBookFromBooks() {
            MakeFreeBookFromBasket(this.Id);
            //App.ShoppingCartViewModel.Books.Remove(this);
            App.ShoppingCartViewModel.Price = App.ShoppingCartViewModel.Books.Sum(x => x.Book.Price * x.Count);
        }

        public static bool FindBook(int idofbook, out Book book) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Книга WHERE Книга.Номер='{idofbook}'", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new Book();
                bool flag = false;
                while (result.Read()) {
                    temp = new Book {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Название"),
                        Price = result.GetFloat("Цена"),
                        Description = result.GetString("Описание"),
                        Genre = result.GetString("Жанр"),
                        Author = result.GetString("Автор"),
                        Image = result.GetString("Фото"),
                        CountBooks = result.GetInt32("Количество")
                    };
                    flag = true;
                }
                con.Close();
                book = temp;
                return flag;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                book = null;
                return false;
            }
        }

        public static Book FindBookReturnClassBook(int idofbook) {
            var bk = new Book();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Книга WHERE Книга.Номер='{idofbook}'", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new Book();
                while (result.Read()) {
                    bk = new Book {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Название"),
                        Price = result.GetFloat("Цена"),
                        Description = result.GetString("Описание"),
                        Genre = result.GetString("Жанр"),
                        Author = result.GetString("Автор"),
                        Image = result.GetString("Фото"),
                        CountBooks = result.GetInt32("Количество")
                    };
                }
                con.Close();
                return bk;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return bk;
            }
        }

        public static bool FindAllAvailableBooks(out List<Book> books) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Книга WHERE Количество>0", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new List<Book>();
                bool flag = false;
                while (result.Read()) {
                    var book = new Book {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Название"),
                        Price = result.GetFloat("Цена"),
                        Description = result.GetString("Описание"),
                        Genre = result.GetString("Жанр"),
                        Author = result.GetString("Автор"),
                        Image = result.GetString("Фото"),
                        CountBooks = result.GetInt32("Количество")
                    };
                    temp.Add(book);
                    flag = true;
                }
                con.Close();
                books = temp;
                return flag;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                books = null;
                return false;
            }
        }

        public static ObservableCollection<Book> FindAllBooks() {
            ObservableCollection<Book> books = new ObservableCollection<Book>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Книга", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    var book = new Book {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Название"),
                        Price = result.GetFloat("Цена"),
                        Description = result.GetString("Описание"),
                        Genre = result.GetString("Жанр"),
                        Author = result.GetString("Автор"),
                        Image = result.GetString("Фото"),
                        CountBooks = result.GetInt32("Количество")
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

        public static List<Book> FindAllBooksReport() {
            List<Book> books = new List<Book>();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM Книга", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    var book = new Book {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Название"),
                        Price = result.GetFloat("Цена"),
                        Description = result.GetString("Описание"),
                        Genre = result.GetString("Жанр"),
                        Author = result.GetString("Автор"),
                        Image = result.GetString("Фото"),
                        CountBooks = result.GetInt32("Количество")
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

        public static bool MakeFreeBookFromBasket(int idofbook) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE Книга SET status = 'Не продана' WHERE Id ={idofbook}", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static bool CheckBookReadyForOrder(int id, int count) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT count(*) FROM Книга WHERE Книга.Номер='{id}' and Книга.Количество>={count}", con);
                int res = Convert.ToInt32(command.ExecuteScalar());
                con.Close();
                if (res != 0)
                    return true;
                return false;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateCountBooks() {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE Книга SET Количество={CountBooks} WHERE Номер={Id}", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static bool InsertNewBook(string name, float? price, string description, string jenre, string author, string photo, int? count) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO Книга VALUES (0,'{name}',{price},'{description}','{jenre}','{author}','http://185.87.50.136/bookshopimages/{photo}',{count})", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static int RemainingBooksCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT SUM(Количество) FROM `Книга`", con);
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
    }
}
