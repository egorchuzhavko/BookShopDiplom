using System;
using System.Diagnostics;
using MySqlConnector;

namespace BookShop.mvvm.Model {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }

        public string Fullname {
            get
            {
                return Surname + " " + Name;
            }
        }

        public static bool CheckForNotExistingEmail(string checkemail) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT count(*) FROM Пользователь WHERE Пользователь.Почта='{checkemail}'", con);
                int res = Convert.ToInt32(command.ExecuteScalar());
                con.Close();
                if (res != 0)
                    return false;
                return true;
            }catch(Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static bool CreateNewUser(string nameuser, string surnameuser, string emailuser, string passworduser) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"INSERT INTO Пользователь VALUES(0,'{nameuser}','{surnameuser}','Покупатель','{passworduser}','{emailuser}','{DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}')", con);
                command.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static bool FindAllUserInfo(string emailuser, string passworduser, out User createdUser) {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM `Пользователь` WHERE `Пользователь`.`Почта`='{emailuser}' and `Пользователь`.`Пароль`='{passworduser}'", con);
                MySqlDataReader result = command.ExecuteReader();
                var temp = new User();
                bool flag = false;
                while (result.Read()) {
                    temp = new User {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Имя"),
                        Surname = result.GetString("Фамилия"),
                        Email = result.GetString("Почта"),
                        Password = result.GetString("Пароль"),
                        Role = result.GetString("Роль"),
                        RegistrationDate = result.GetDateTime("ДатаРегистрации")
                    };
                    flag = true;
                }
                con.Close();
                createdUser = temp;
                return flag;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                createdUser = new User();
                return false;
            }
        }

        public static User FindAllUserInfoById(int id) {
            var user = new User();

            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT * FROM `Пользователь` WHERE Номер={id}", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    user = new User {
                        Id = result.GetInt32("Номер"),
                        Name = result.GetString("Имя"),
                        Surname = result.GetString("Фамилия"),
                        Email = result.GetString("Почта"),
                        Password = result.GetString("Пароль"),
                        Role = result.GetString("Роль"),
                        RegistrationDate = result.GetDateTime("ДатаРегистрации")
                    };
                }
                con.Close();
                return user;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                return user;
            }
        }

        public static int UsersCount() {
            int count = 0;
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) FROM `Пользователь`", con);
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

        public bool UpdateInfoUser() {
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"UPDATE Пользователь SET Имя='{Name}', Фамилия='{Surname}', Пароль='{MdFive.GetHash(Password)}' WHERE Номер={Id}", con);
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
