using System;
using System.Collections.Generic;
using Renci.SshNet;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BookShop.mvvm.Model
{
    class SFTPService
    {
        public static async Task<string> SaveToCacheAsync(Stream data, string fileName) {
            var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
            using (var stream = File.Create(filePath)) {
                await data.CopyToAsync(stream);
            }

            return filePath;
        }

        public static async Task<bool> UploadImage(string uploadfile, string fileName) {
            using (SftpClient sftpClient = new SftpClient("185.87.50.136", 22, "root", "tVGN4lBQ4G3t")) {
                sftpClient.Connect();
                sftpClient.ChangeDirectory("/var/www/html/bookshopimages");
                using (var fileStream = new FileStream(uploadfile, FileMode.Open)) {
                    sftpClient.BufferSize = 122256;
                    sftpClient.UploadFile(fileStream, $"{fileName}");
                    sftpClient.Dispose();
                }
                sftpClient.Dispose();
            }
            return true;
        }
    }
}
