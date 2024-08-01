using eShopSolution.ViewModels.Catalog.Categories; // Import namespace của các view model danh mục để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Catalog.Products; // Import namespace của các view model sản phẩm để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Common; // Import namespace của các view model chung để sử dụng các view model liên quan.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.

namespace eShopSolution.Application.Catalog.Categories // Khai báo namespace cho interface ICategoryService.
{
    public interface ICategoryService // Định nghĩa interface ICategoryService để quản lý các dịch vụ liên quan đến danh mục sản phẩm.
    {
        Task<int> Create(CategoryCreateRequest request); // Phương thức tạo mới một danh mục sản phẩm.

        Task<int> Update(CategoryUpdateRequest request); // Phương thức cập nhật thông tin của một danh mục sản phẩm.

        Task<int> Delete(int categoryId); // Phương thức xóa một danh mục sản phẩm.

        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetManageProductPagingRequest request); // Phương thức lấy danh sách các danh mục sản phẩm phân trang.

        Task<CategoryViewModel> GetById(int id); // Phương thức lấy thông tin của một danh mục sản phẩm dựa trên ID.

        Task<List<CategoryViewModel>> GetAll(); // Phương thức lấy danh sách tất cả các danh mục sản phẩm.
    }
}
