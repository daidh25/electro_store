using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Coupons
{
    // Giao diện ICouponService định nghĩa các phương thức mà một dịch vụ quản lý phiếu giảm giá cần triển khai.
    public interface ICouponService
    {
        // Phương thức tạo mới một phiếu giảm giá dựa trên dữ liệu từ yêu cầu CouponCreateRequest.
        Task<int> Create(CouponCreateRequest request);

        // Phương thức cập nhật thông tin của một phiếu giảm giá hiện có dựa trên dữ liệu từ yêu cầu CouponUpdateRequest.
        Task<int> Update(CouponUpdateRequest request);

        // Phương thức xóa một phiếu giảm giá khỏi cơ sở dữ liệu dựa trên id của phiếu giảm giá.
        Task<int> Delete(int couponId);

        // Phương thức trả về một trang của các phiếu giảm giá, sắp xếp và lọc dựa trên các yêu cầu được chỉ định trong GetManageProductPagingRequest.
        Task<PagedResult<CouponViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        // Phương thức trả về danh sách tất cả các phiếu giảm giá trong cơ sở dữ liệu.
        Task<List<CouponViewModel>> GetAll();

        // Phương thức lấy thông tin của một phiếu giảm giá dựa trên id.
        Task<CouponViewModel> GetById(int id);
    }
}
// Trong ngữ cảnh của mô hình lập trình bất đồng bộ (asynchronous programming), việc sử dụng Task cho phép chúng ta thực hiện các tác
// vụ mà không cần chờ đợi tác vụ trước đó hoàn thành.