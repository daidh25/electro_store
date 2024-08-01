using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Products // Khai báo namespace cho lớp ReviewViewModel.
{
    public class ReviewViewModel // Định nghĩa lớp ReviewViewModel để đại diện cho thông tin của một đánh giá về sản phẩm.
    {
        public int Id { get; set; } // Thuộc tính để lưu trữ ID của đánh giá.

        public Guid UserId { set; get; } // Thuộc tính để lưu trữ ID của người dùng thực hiện đánh giá.

        public string UserName { get; set; } // Thuộc tính để lưu trữ tên của người dùng thực hiện đánh giá.

        public int ProductId { get; set; } // Thuộc tính để lưu trữ ID của sản phẩm mà đánh giá này liên quan đến.

        public int Rating { get; set; } // Thuộc tính để lưu trữ đánh giá (số điểm) của sản phẩm.

        public string Comments { get; set; } // Thuộc tính để lưu trữ nhận xét của người dùng về sản phẩm.

        public DateTime PublishedDate { get; set; } // Thuộc tính để lưu trữ ngày và giờ công bố của đánh giá.
    }
}
