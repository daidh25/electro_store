using System; // Import namespace cho các kiểu dữ liệu cơ bản và hệ thống.
using System.Collections.Generic; // Import namespace cho các collection.
using System.IO; // Import namespace để thao tác với các tệp tin và thư mục.
using System.Text; // Import namespace cho các công cụ xử lý chuỗi.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.

namespace eShopSolution.Application.Common // Khai báo namespace cho giao diện IStorageService.
{
    public interface IStorageService // Định nghĩa giao diện IStorageService.
    {
        string GetFileUrl(string fileName); // Phương thức lấy URL của tệp tin dựa trên tên tệp.

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName); // Phương thức lưu tệp tin vào thư mục lưu trữ.

        Task DeleteFileAsync(string fileName); // Phương thức xóa tệp tin từ thư mục lưu trữ.
    }
}
