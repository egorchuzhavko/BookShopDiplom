using GemBox.Document;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BookShop.mvvm.Model {
    public class ReportUser {
        public int CountOfUsers { get; set; }
        public int CountOrders { get; set; }
        public int CountFeedBack { get; set; }
        public int CountAllReports { get; set; }
        public int CountAnsweredReports { get; set; }

        public static DocumentModel FillDocumentDefaultInfo(DocumentModel document, DateTime datefrom, DateTime dateto) {
            var rb = TakeDefaultInfo();
            var dtd = TakeInfoInDateTime(datefrom, dateto);
            SpecialCharacter lineBreakElement = new SpecialCharacter(document, SpecialCharacterType.LineBreak);
            document.Sections.Add(
                new Section(document,
                new Paragraph(document,
                new Run(document, "Отчёт по пользователям") { CharacterFormat = { Bold = true } }) {
                    ParagraphFormat = {
                            Alignment=HorizontalAlignment.Center
                        }
                },
                new Paragraph(document,
                new Run(document, $"Общая статистика:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Всего пользователей: {rb.CountOfUsers}"),
                lineBreakElement.Clone(),
                new Run(document, $"Всего совершенных заказов: {rb.CountOrders}"),
                lineBreakElement.Clone(),
                new Run(document, $"Всего отзывов: {rb.CountFeedBack}"),
                lineBreakElement.Clone(),
                new Run(document, $"Количество запросов в поддержку: {rb.CountAllReports}"),
                lineBreakElement.Clone(),
                new Run(document, $"Количество запросов в поддержку, на которые дали ответ: {rb.CountAnsweredReports}"),
                lineBreakElement.Clone(), lineBreakElement.Clone()),
                 new Paragraph(document,
                new Run(document, $"Статистика в период с {datefrom.ToLongDateString()} по {dateto.ToLongDateString()}:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Количество зарегистрированных пользователей: {dtd.CountUsersInDate}"),
                lineBreakElement.Clone(),
                new Run(document, $"Количество заказов: {dtd.CountOrdersInDate}"),
                lineBreakElement.Clone()))
                );
            return document;
        }

        public static ReportUser TakeDefaultInfo() {
            var rb = new ReportUser();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT (SELECT COUNT(*) FROM `Пользователь`) as КолвоПользователь, (SELECT COUNT(*) FROM `Заказ`) as КолвоЗаказ, (SELECT COUNT(*) FROM `Отзыв`) as КолвоОтзыв, (SELECT COUNT(*) FROM Поддержка) as КолвоЗапросовВПоддержку, (SELECT COUNT(*) FROM Поддержка WHERE `Поддержка`.`Ответ`!='-') as КолвоОтвеченныхЗапросов FROM `Пользователь` limit 1 ", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    rb = new ReportUser {
                        CountOfUsers = result.GetInt32(0),
                        CountOrders = result.GetInt32(1),
                        CountFeedBack = result.GetInt32(2),
                        CountAllReports = result.GetInt32(3),
                        CountAnsweredReports = result.GetInt32(4)
                    };
                }
                con.Close();
                return rb;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return rb;
            }
        }

        public static ClassUserInDate TakeInfoInDateTime(DateTime df, DateTime dt) {
            var rb = new ClassUserInDate();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT (SELECT COUNT(*) FROM `Пользователь` WHERE `Пользователь`.`ДатаРегистрации`>='{df.ToString("yyyy-MM-dd")}' and `Пользователь`.`ДатаРегистрации`<='{dt.ToString("yyyy-MM-dd")}') as КолвоПользователей, (select Count(*) from `Заказ` WHERE `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}') as КолВоЗаказов  FROM `Пользователь` LIMIT 1", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    rb = new ClassUserInDate {
                        CountUsersInDate = result.GetInt32(0),
                        CountOrdersInDate = result.GetInt32(1)
                    };
                }
                con.Close();
                return rb;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return rb;
            }
        }
    }

    public class ClassUserInDate {
        public int CountUsersInDate { get; set; }
        public int CountOrdersInDate { get; set; }
    }
}
