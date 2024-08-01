using Microsoft.AspNetCore.Http; // Import namespace của Microsoft.AspNetCore.Http để sử dụng các thành phần liên quan đến HTTP trong ASP.NET Core.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.ProductImages // Khai báo namespace cho lớp ProductImageViewModel.
{
    public class ProductImageViewModel // Định nghĩa lớp ProductImageViewModel để đại diện cho một hình ảnh sản phẩm trong giao diện người dùng.
    {
        public int Id { get; set; } // Thuộc tính để lưu trữ ID của hình ảnh.

        public int ProductId { get; set; } // Thuộc tính để lưu trữ ID của sản phẩm mà hình ảnh này thuộc về.

        public string ImagePath { get; set; } // Đường dẫn tới hình ảnh.

        public string Caption { get; set; } // Mô tả cho hình ảnh.

        public bool IsDefault { get; set; } // Đánh dấu xem hình ảnh này có phải là hình ảnh mặc định của sản phẩm không.

        public DateTime DateCreated { get; set; } // Ngày tạo hình ảnh.

        public int SortOrder { get; set; } // Thứ tự sắp xếp của hình ảnh trong danh sách các hình ảnh của sản phẩm.

        public long FileSize { get; set; } // Kích thước của file hình ảnh (đơn vị: byte).
    }
}