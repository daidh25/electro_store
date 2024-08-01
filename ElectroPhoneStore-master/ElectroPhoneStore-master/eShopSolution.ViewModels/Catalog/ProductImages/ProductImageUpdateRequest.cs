using Microsoft.AspNetCore.Http; // Import namespace của Microsoft.AspNetCore.Http để sử dụng các thành phần liên quan đến HTTP trong ASP.NET Core.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.ProductImages // Khai báo namespace cho lớp ProductImageUpdateRequest.
{
    public class ProductImageUpdateRequest // Định nghĩa lớp ProductImageUpdateRequest để đại diện cho yêu cầu cập nhật thông tin hình ảnh sản phẩm.
    {
        public int Id { get; set; } // Thuộc tính để lưu trữ ID của hình ảnh sản phẩm cần cập nhật.

        public string Caption { get; set; } // Thuộc tính để lưu trữ mô tả cho hình ảnh sản phẩm.

        public bool IsDefault { get; set; } // Thuộc tính để đánh dấu xem hình ảnh có phải là hình ảnh mặc định của sản phẩm không.

        public int SortOrder { get; set; } // Thuộc tính để lưu trữ thứ tự sắp xếp của hình ảnh trong danh sách các hình ảnh của sản phẩm.

        public IFormFile ImageFile { get; set; } // Thuộc tính để lưu trữ đối tượng IFormFile chứa dữ liệu của hình ảnh mới được tải lên từ giao diện người dùng.
    }
}