using System;
using System.Collections.Generic;
using System.Text;
using MySqlConnector;
using GemBox.Document;
using System.Diagnostics;
using System.Linq;

namespace BookShop.mvvm.Model {
    public class ReportBook {
        public int CountTypesBook { get; set; }
        public int CountAllBooks { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }

        public static DocumentModel FillDocumentDefaultInfo(DocumentModel document, DateTime datefrom, DateTime dateto) {
            var rb = TakeDefaultInfo();
            var allbooks = Book.FindAllBooksReport();
            string strbookscount = "";
            foreach (var book in allbooks) {
                strbookscount += $"{book.Name} - цена {book.Price} руб. - {book.CountBooks} шт. ; ";
            }
            var listofids = Order.FindIdsOfOrdersInDateTime(datefrom, dateto);
            int countselled = 0;
            double allprice = 0;
            if (listofids.Count != 0) {
                allprice = Order.FindSumPriceForOrders(string.Join(",", listofids));
                countselled = BookOrder.FindSumCountBooksSelled(string.Join(",", listofids));
            }
            SpecialCharacter lineBreakElement = new SpecialCharacter(document, SpecialCharacterType.LineBreak);
            document.Sections.Add(
                new Section(document,
                new Paragraph(document,
                new Run(document, "Отчёт по книгам") { CharacterFormat = { Bold = true } }) {
                    ParagraphFormat = {
                            Alignment=HorizontalAlignment.Center
                        }
                },
                new Paragraph(document,
                new Run(document, $"Общая статистика:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Всего видов книг: {rb.CountTypesBook}"),
                lineBreakElement.Clone(),
                new Run(document, $"Общее количество книг: {rb.CountAllBooks}"),
                lineBreakElement.Clone(),
                new Run(document, $"Максимальная цена за книгу: {rb.MaxPrice}"),
                lineBreakElement.Clone(),
                new Run(document, $"Минимальная цена за книгу: {rb.MinPrice}"),
                lineBreakElement.Clone(), lineBreakElement.Clone(),

                new Run(document, strbookscount)),
                 new Paragraph(document,
                new Run(document, $"Статистика в период с {datefrom.ToLongDateString()} по {dateto.ToLongDateString()}:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Всего куплено книг: {countselled}"),
                lineBreakElement.Clone(),
                new Run(document, $"Продано книг на сумму: {allprice.ToString("0.00")} рублей"),
                lineBreakElement.Clone()))
                );
            return document;
        }

        public static ReportBook TakeDefaultInfo() {
            var rb = new ReportBook();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT (select COUNT(*) from Книга) as 'КолвоВидовКниг', (select SUM(`Книга`.`Количество`) from `Книга`) as 'ОбщееКолвоКниг', (select max(Цена) from Книга) as 'МаксЦенаЗаКнигу',(select min(Цена) from Книга) as 'МинЦенаЗаКнигу' FROM `Книга` limit 1", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    rb = new ReportBook {
                        CountTypesBook = result.GetInt32(0),
                        CountAllBooks = result.GetInt32(1),
                        MaxPrice = result.GetDouble(2),
                        MinPrice = result.GetDouble(3)
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
}
