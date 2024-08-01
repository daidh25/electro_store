using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    // Interface này tạo các phương thức để quản lý sản phẩm
    public interface IProductService
    {
        // Phương thức tạo mới sản phẩm
        // Trả về mã sản phẩm vừa tạo
        // Tham số không nhất thiết phải truyền vào một Product view model, có thể bị thừa
        Task<int> Create(ProductCreateRequest request);

        // Phương thức cập nhật thông tin sản phẩm
        // Truyền một DTO (Data Transfer Object) vào phương thức
        Task<int> Update(ProductUpdateRequest request);

        // Phương thức xóa sản phẩm bằng cách truyền vào ID của sản phẩm
        Task<int> Delete(int productId);

        // Lấy thông tin sản phẩm bằng ID
        Task<ProductViewModel> GetById(int productId);

        // Phương thức tìm kiếm sản phẩm
        // Trả về một danh sách sản phẩm được phân trang
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // Lấy tất cả sản phẩm theo ID của danh mục
        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        // Lấy danh sách sản phẩm nổi bật
        Task<List<ProductViewModel>> GetFeaturedProducts(int take);

        // Lấy danh sách sản phẩm mới nhất
        Task<List<ProductViewModel>> GetLatestProducts(int take);

        // Giảm số lượng tồn kho của sản phẩm
        Task<bool> DecreaseStock(int productId, int quantity);

        // Thêm nhận xét về sản phẩm
        Task<int> AddReview(ProductDetailViewModel model);
    }
}