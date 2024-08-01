using Microsoft.AspNetCore.Http; // Import namespace của Microsoft.AspNetCore.Http để sử dụng các thành phần liên quan đến HTTP trong ASP.NET Core.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.ProductImages // Khai báo namespace cho lớp ProductImageCreateRequest.
{
    // Định nghĩa lớp ProductImageCreateRequest để đại diện cho yêu cầu tạo mới hình ảnh sản phẩm.
    public class ProductImageCreateRequest 
    {
        // Không có chú thích ở đây vì các thuộc tính đã được chú thích ở phần dưới.

        //public int ProductId { get; set; } // Thuộc tính để lưu trữ ID của sản phẩm. Đã được loại bỏ vì tham số productId được truyền vào từ bên ngoài.

        public string Caption { get; set; } // Thuộc tính để lưu trữ mô tả cho hình ảnh sản phẩm.

        public bool IsDefault { get; set; } // Thuộc tính để đánh dấu xem hình ảnh có phải là hình ảnh mặc định của sản phẩm không.

        public int SortOrder { get; set; } // Thuộc tính để lưu trữ thứ tự sắp xếp của hình ảnh trong danh sách các hình ảnh của sản phẩm.

        public IFormFile ImageFile { get; set; } // Thuộc tính để lưu trữ đối tượng IFormFile chứa dữ liệu của hình ảnh được tải lên từ giao diện người dùng.
    }
}