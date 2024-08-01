using eShopSolution.ViewModels.Common; // Import namespace để sử dụng các view model thông thường được chia sẻ trong ứng dụng.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Products // Khai báo namespace cho lớp GetManageProductPagingRequest.
{
    public class GetManageProductPagingRequest : PagingRequestBase // Định nghĩa lớp GetManageProductPagingRequest để đại diện cho yêu cầu phân trang và lọc sản phẩm.
    {
        public string Keyword { get; set; } // Thuộc tính để lưu trữ từ khóa tìm kiếm sản phẩm.

        public int? CategoryId { get; set; } // Thuộc tính để lưu trữ ID của danh mục sản phẩm để lọc.

        public string? SortOption { get; set; } // Thuộc tính để lưu trữ tùy chọn sắp xếp sản phẩm (có thể là null).
    }
}
