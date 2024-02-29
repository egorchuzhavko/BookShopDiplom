using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using GemBox.Document;
using BookShop.mvvm.Model;

[assembly: Dependency(typeof(SaveAndroid))]
class SaveAndroid : ISave {
    public async Task SaveAndView(DateTime datefrom, DateTime dateto, string parameter) {

        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        var document = new DocumentModel();
        if (parameter == "Все") {
            document = ReportBook.FillDocumentDefaultInfo(document, datefrom, dateto);
            document = ReportOrder.FillDocumentDefaultInfo(document, datefrom, dateto);
            document = ReportUser.FillDocumentDefaultInfo(document, datefrom, dateto);
        }
        else {
            if (parameter == "Книга") document = ReportBook.FillDocumentDefaultInfo(document, datefrom, dateto);
            if (parameter == "Заказ") document = ReportOrder.FillDocumentDefaultInfo(document, datefrom, dateto);
            if (parameter == "Пользователь") document = ReportUser.FillDocumentDefaultInfo(document, datefrom, dateto);
        }
        DateTime now = DateTime.Now;
        string namedoc = "Отчёт-" + parameter + "-" + now.ToString("yyyy-MM-dd-HH-mm-ss") + ".docx";
        var filePath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(type: Android.OS.Environment.DirectoryDocuments)?.AbsolutePath, namedoc);

        document.Save(filePath);
        await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Успех", "Отчёт был успешно создан! Он находится в проводнике в папке 'Documents'.", "Ок");

    }
}
