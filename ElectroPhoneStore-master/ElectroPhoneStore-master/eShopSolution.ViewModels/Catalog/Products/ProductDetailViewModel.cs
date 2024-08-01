using eShopSolution.ViewModels.Catalog.Categories; // Import namespace của các view model danh mục để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Catalog.ProductImages; // Import namespace của các view model hình ảnh sản phẩm để sử dụng các view model liên quan.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Products // Khai báo namespace cho lớp ProductDetailViewModel.
{
    public class ProductDetailViewModel // Định nghĩa lớp ProductDetailViewModel để đại diện cho thông tin chi tiết của một sản phẩm.
    {
        public CategoryViewModel Category { get; set; } // Thuộc tính để lưu trữ thông tin về danh mục của sản phẩm.

        public ProductViewModel Product { get; set; } // Thuộc tính để lưu trữ thông tin về sản phẩm.

        //public List<ReviewViewModel> Reviews { get; set; } // Không sử dụng trong mã mẫu.

        public List<ReviewViewModel> ListOfReviews { get; set; } // Thuộc tính để lưu trữ danh sách các đánh giá về sản phẩm.

        public string Review { get; set; } // Thuộc tính để lưu trữ nội dung đánh giá sản phẩm.

        public int ProductId { get; set; } // Thuộc tính để lưu trữ ID của sản phẩm.

        public int Rating { get; set; } // Thuộc tính để lưu trữ đánh giá về sản phẩm.

        // Use to get user id when add review
        public string UserCommentId { get; set; } // Thuộc tính để lưu trữ ID của người dùng khi thêm đánh giá.

        public List<ProductViewModel> RelatedProducts { get; set; } // Thuộc tính để lưu trữ danh sách các sản phẩm liên quan.

        public List<ProductImageViewModel> ProductImages { get; set; } // Thuộc tính để lưu trữ danh sách các hình ảnh của sản phẩm.
    }
}
