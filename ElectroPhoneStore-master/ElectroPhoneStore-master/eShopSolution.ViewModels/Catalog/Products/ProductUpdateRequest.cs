using eShopSolution.ViewModels.Catalog.Categories; // Import namespace của các view model danh mục để sử dụng các view model liên quan.
using Microsoft.AspNetCore.Http; // Import namespace của Microsoft.AspNetCore.Http để sử dụng các thành phần liên quan đến HTTP trong ASP.NET Core.
using Microsoft.AspNetCore.Mvc.Rendering; // Import namespace để sử dụng các lớp hỗ trợ việc tạo ra các điều khiển (controls) HTML trong MVC.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.ComponentModel.DataAnnotations; // Import namespace để sử dụng các annotation để xác định metadata của các thuộc tính.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Products // Khai báo namespace cho lớp ProductUpdateRequest.
{
    // thường ta chỉ update các property trong translation
    public class ProductUpdateRequest // Định nghĩa lớp ProductUpdateRequest để đại diện cho yêu cầu cập nhật thông tin của một sản phẩm.
    {
        public int Id { set; get; } // Thuộc tính để lưu trữ ID của sản phẩm cần cập nhật.

        [Display(Name = "Tên sản phẩm")] // Xác định tên hiển thị cho thuộc tính Name khi được sử dụng trong giao diện người dùng.
        public string Name { get; set; } // Thuộc tính để lưu trữ tên của sản phẩm.

        [Display(Name = "Danh mục")] // Xác định tên hiển thị cho thuộc tính CategoryId khi được sử dụng trong giao diện người dùng.
        public int CategoryId { set; get; } // Thuộc tính để lưu trữ ID của danh mục mà sản phẩm thuộc về.

        [Display(Name = "Giá tiền")] // Xác định tên hiển thị cho thuộc tính Price khi được sử dụng trong giao diện người dùng.
        public decimal Price { get; set; } // Thuộc tính để lưu trữ giá tiền của sản phẩm.

        [Display(Name = "Số lượng")] // Xác định tên hiển thị cho thuộc tính Stock khi được sử dụng trong giao diện người dùng.
        public int Stock { set; get; } // Thuộc tính để lưu trữ số lượng tồn kho của sản phẩm.

        [Display(Name = "Thông số kỹ thuật")] // Xác định tên hiển thị cho thuộc tính Description khi được sử dụng trong giao diện người dùng.
        public string Description { set; get; } // Thuộc tính để lưu trữ thông số kỹ thuật của sản phẩm.

        [Display(Name = "Mô tả chi tiết")] // Xác định tên hiển thị cho thuộc tính Details khi được sử dụng trong giao diện người dùng.
        public string Details { set; get; } // Thuộc tính để lưu trữ mô tả chi tiết về sản phẩm.

        [Display(Name = "Ảnh đại diện")] // Xác định tên hiển thị cho thuộc tính ThumbnailImage khi được sử dụng trong giao diện người dùng.
        public IFormFile ThumbnailImage { get; set; } // Thuộc tính để lưu trữ đối tượng IFormFile chứa hình ảnh đại diện của sản phẩm.

        [Display(Name = "Ảnh đầy đủ")] // Xác định tên hiển thị cho thuộc tính ProductImage khi được sử dụng trong giao diện người dùng.
        public IFormFile ProductImage { get; set; } // Thuộc tính để lưu trữ đối tượng IFormFile chứa hình ảnh đầy đủ của sản phẩm.

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>(); // Thuộc tính để lưu trữ danh sách các danh mục sản phẩm, được gán mặc định là một danh sách trống.
    }
}
