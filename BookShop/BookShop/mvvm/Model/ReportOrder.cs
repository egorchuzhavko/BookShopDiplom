using GemBox.Document;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BookShop.mvvm.Model {
    public class ReportOrder {
        public int AllCount { get; set; }
        public double TotalSum { get; set; }
        public int CountOformlen { get; set; }
        public int CountPrinyat { get; set; }
        public int CountVPyti { get; set; }
        public int CountDostavlen { get; set; }
        public int CountZaverwen { get; set; }
        public int CountOtmenen { get; set; }

        public static DocumentModel FillDocumentDefaultInfo(DocumentModel document, DateTime datefrom, DateTime dateto) {
            var rb = TakeDefaultInfo();
            var rbinperiod = TakeInfoInPeriod(datefrom, dateto);
            SpecialCharacter lineBreakElement = new SpecialCharacter(document, SpecialCharacterType.LineBreak);
            document.Sections.Add(
                new Section(document,
                new Paragraph(document,
                new Run(document, "Отчёт по заказам") { CharacterFormat = { Bold = true } }) {
                    ParagraphFormat = {
                            Alignment=HorizontalAlignment.Center
                        }
                },
                new Paragraph(document,
                new Run(document, $"Общая статистика:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Всего заказов: {rb.AllCount}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов на сумму: {rb.TotalSum.ToString("0.00")} рублей"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Оформлен': {rb.CountOformlen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Принят': {rb.CountPrinyat}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'В пути': {rb.CountVPyti}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Доставлен': {rb.CountDostavlen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Завершён': {rb.CountZaverwen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Отменён': {rb.CountOtmenen}"),
                lineBreakElement.Clone(),
                lineBreakElement.Clone()),

                new Paragraph(document,
                new Run(document, $"Статистика в период с {datefrom.ToLongDateString()} по {dateto.ToLongDateString()}:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Общая статистика:") { CharacterFormat = { Bold = true } },
                lineBreakElement.Clone(),
                new Run(document, $"Всего заказов: {rbinperiod.AllCount}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов на сумму: {rbinperiod.TotalSum.ToString("0.00")} рублей"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Оформлен': {rbinperiod.CountOformlen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Принят': {rbinperiod.CountPrinyat}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'В пути': {rbinperiod.CountVPyti}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Доставлен': {rbinperiod.CountDostavlen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Завершён': {rbinperiod.CountZaverwen}"),
                lineBreakElement.Clone(),
                new Run(document, $"Заказов со статусом 'Отменён': {rbinperiod.CountOtmenen}"),
                lineBreakElement.Clone()
                ))
                );
            return document;
        }

        public static ReportOrder TakeDefaultInfo() {
            var rb = new ReportOrder();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT COUNT(*) as КолВо, SUM(Цена) as Сумма, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Оформлен')as КолвоОформлен, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Принят')as КолвоПринят, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='В пути')as КолвоВПути, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Доставлен')as КолвоДоставлен, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Завершён')as КолвоЗавершён, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Отменён')as КолвоОтменён FROM `Заказ`", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    rb = new ReportOrder {
                        AllCount = result.GetInt32(0),
                        TotalSum = result.GetDouble(1),
                        CountOformlen = result.GetInt32(2),
                        CountPrinyat = result.GetInt32(3),
                        CountVPyti = result.GetInt32(4),
                        CountDostavlen = result.GetInt32(5),
                        CountZaverwen = result.GetInt32(6),
                        CountOtmenen = result.GetInt32(7)
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

        public static ReportOrder TakeInfoInPeriod(DateTime df, DateTime dt) {
            var rb = new ReportOrder();
            try {
                string connStr = "server=185.87.50.136;user=**********;database=КнижныйМагазин;password=**********;";
                MySqlConnection con = new MySqlConnection(connStr);
                con.Open();
                MySqlCommand command = new MySqlCommand($"SELECT (select Count(*) from `Заказ` WHERE `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}') as КолВо, (select SUM(Цена) from `Заказ` WHERE `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}') as Сумма, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Оформлен' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоОформлен, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Принят' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоПринят, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='В пути' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоВПути, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Доставлен' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоДоставлен, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Завершён' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоЗавершён, (SELECT COUNT(*) from Заказ WHERE `Заказ`.`Статус`='Отменён' and `Заказ`.`ДатаЗаказа`>='{df.ToString("yyyy-MM-dd")}' and `Заказ`.`ДатаИзмененияСтатусаЗаказа`<='{dt.ToString("yyyy-MM-dd")}')as КолвоОтменён FROM `Заказ`", con);
                MySqlDataReader result = command.ExecuteReader();
                while (result.Read()) {
                    rb = new ReportOrder {
                        AllCount = result.GetInt32(0),
                        TotalSum = result.GetDouble(1),
                        CountOformlen = result.GetInt32(2),
                        CountPrinyat = result.GetInt32(3),
                        CountVPyti = result.GetInt32(4),
                        CountDostavlen = result.GetInt32(5),
                        CountZaverwen = result.GetInt32(6),
                        CountOtmenen = result.GetInt32(7)
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
