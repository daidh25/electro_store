using Microsoft.AspNetCore.Hosting; // Import namespace để sử dụng IWebHostEnvironment.
using System; // Import namespace cho các kiểu dữ liệu cơ bản và hệ thống.
using System.Collections.Generic; // Import namespace cho các collection.
using System.IO; // Import namespace để thao tác với các tệp tin và thư mục.
using System.Text; // Import namespace cho các công cụ xử lý chuỗi.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.

namespace eShopSolution.Application.Common // Khai báo namespace cho lớp FileStorageService.
{
    public class FileStorageService : IStorageService // Định nghĩa lớp FileStorageService, triển khai giao diện IStorageService.
    {
        private readonly string _userContentFolder; // Trường để lưu trữ đường dẫn đến thư mục lưu trữ tệp tin của người dùng.
        private const string USER_CONTENT_FOLDER_NAME = "user-content"; // Tên thư mục lưu trữ tệp tin của người dùng.

        public FileStorageService(IWebHostEnvironment webHostEnvironment) // Constructor nhận đối tượng IWebHostEnvironment.
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME); // Xác định đường dẫn đến thư mục lưu trữ tệp tin của người dùng.
        }

        // Phương thức lấy URL của tệp tin dựa trên tên tệp.
        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}"; // Trả về URL của tệp tin.
        }

        // Phương thức lưu tệp tin vào thư mục lưu trữ.
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName); // Xác định đường dẫn đến tệp tin.
            using var output = new FileStream(filePath, FileMode.Create); // Mở tệp tin để ghi dữ liệu vào.
            await mediaBinaryStream.CopyToAsync(output); // Sao chép dữ liệu từ luồng đầu vào vào luồng đầu ra.
        }

        // Phương thức xóa tệp tin từ thư mục lưu trữ.
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName); // Xác định đường dẫn đến tệp tin cần xóa.
            if (File.Exists(filePath)) // Kiểm tra xem tệp tin có tồn tại không.
            {
                await Task.Run(() => File.Delete(filePath)); // Xóa tệp tin bất đồng bộ.
            }
        }
    }
}
