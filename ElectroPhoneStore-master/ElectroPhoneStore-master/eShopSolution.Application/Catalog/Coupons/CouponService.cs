using eShopSolution.Data.EF; // Import namespace để sử dụng EShopDbContext để tương tác với cơ sở dữ liệu.
using eShopSolution.Data.Entities; // Import namespace để sử dụng các thực thể (entities) trong cơ sở dữ liệu.
using eShopSolution.Utilities.Exceptions; // Import namespace để sử dụng các lớp xử lý ngoại lệ tùy chỉnh.
using eShopSolution.ViewModels.Sales; // Import namespace của các view model phiếu giảm giá để sử dụng các view model liên quan.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.
using System.Linq; // Import namespace để sử dụng LINQ để truy vấn dữ liệu.
using Microsoft.EntityFrameworkCore; // Import namespace để sử dụng các lớp hỗ trợ cho Entity Framework Core.
using eShopSolution.ViewModels.Common; // Import namespace của các view model chung để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Catalog.Products; // Import namespace của các view model sản phẩm để sử dụng các view model liên quan.

namespace eShopSolution.Application.Catalog.Coupons // Khai báo namespace cho lớp CouponService.
{
    public class CouponService : ICouponService // Định nghĩa lớp CouponService, triển khai giao diện ICouponService.
    {
        private readonly EShopDbContext _context; // Trường để lưu trữ đối tượng EShopDbContext cho việc truy cập cơ sở dữ liệu.

        public CouponService(EShopDbContext context) // Constructor nhận một tham số là đối tượng EShopDbContext.
        {
            _context = context; // Gán giá trị của tham số context cho trường _context.
        }

        public async Task<int> Create(CouponCreateRequest request) // Phương thức để tạo mới một phiếu giảm giá.
        {
            var coupon = new Coupon() // Tạo mới một đối tượng Coupon từ dữ liệu trong request.
            {
                Code = request.Code, // Gán mã code từ dữ liệu trong request.
                Count = request.Count, // Gán số lần sử dụng từ dữ liệu trong request.
                Promotion = request.Promotion, // Gán mức giảm giá từ dữ liệu trong request.
                Describe = request.Describe // Gán mô tả từ dữ liệu trong request.
            };

            _context.Coupons.Add(coupon); // Thêm coupon vào DbSet Coupons của _context.
            await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu.
            return coupon.Id; // Trả về ID của coupon vừa được tạo.
        }

        public async Task<int> Update(CouponUpdateRequest request) // Phương thức để cập nhật thông tin của một phiếu giảm giá.
        {
            var coupon = await _context.Coupons.FindAsync(request.Id); // Tìm coupon cần cập nhật dựa trên ID từ request.
            if (coupon == null) throw new EShopException($"Không thể tìm coupon có ID: {request.Id} "); // Nếu không tìm thấy coupon, ném một ngoại lệ EShopException.

            coupon.Code = request.Code; // Cập nhật mã code từ dữ liệu trong request.
            coupon.Count = request.Count; // Cập nhật số lần sử dụng từ dữ liệu trong request.
            coupon.Promotion = request.Promotion; // Cập nhật mức giảm giá từ dữ liệu trong request.
            coupon.Describe = request.Describe; // Cập nhật mô tả từ dữ liệu trong request.

            return await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu và trả về số lượng bản ghi đã được cập nhật.
        }

        public async Task<int> Delete(int couponId) // Phương thức để xóa một phiếu giảm giá.
        {
            var coupon = await _context.Coupons.FindAsync(couponId); // Tìm coupon cần xóa dựa trên ID.
            if (coupon == null) throw new EShopException($"Không thể tìm coupon có ID: {couponId} "); // Nếu không tìm thấy coupon, ném một ngoại lệ EShopException.

            _context.Coupons.Remove(coupon); // Xóa coupon khỏi DbSet Coupons của _context.

            return await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu và trả về số lượng bản ghi đã được xóa.
        }

        public async Task<PagedResult<CouponViewModel>> GetAllPaging(GetManageProductPagingRequest request) // Phương thức để lấy danh sách các phiếu giảm giá phân trang.
        {
            var query = from c in _context.Coupons // Tạo một truy vấn LINQ để lấy danh sách các phiếu giảm giá từ DbSet Coupons của _context.
                        select new { c };

            if (!string.IsNullOrEmpty(request.Keyword)) // Nếu có từ khóa tìm kiếm.
                query = query.Where(x => x.c.Code.Contains(request.Keyword)); // Lọc danh sách các phiếu giảm giá dựa trên từ khóa.

            // Phân trang
            int totalRow = await query.CountAsync(); // Đếm tổng số phiếu giảm giá.

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize) // Bỏ qua các bản ghi không cần thiết.
                .Take(request.PageSize) // Lấy số lượng bản ghi cần thiết.
                .Select(x => new CouponViewModel() // Chuyển các phiếu giảm giá sang dạng ViewModel.
                {
                    Id = x.c.Id,
                    Code = x.c.Code,
                    Count = x.c.Count,
                    Promotion = x.c.Promotion,
                    Describe = x.c.Describe
                }).ToListAsync(); // Tải danh sách kết quả vào bộ nhớ.

            // Tạo đối tượng kết quả phân trang.
            var pagedResult = new PagedResult<CouponViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult; // Trả về kết quả phân trang.
        }

        public async Task<List<CouponViewModel>> GetAll() // Phương thức để lấy danh sách tất cả các phiếu giảm giá.
        {
            var query = from c in _context.Coupons // Tạo một truy vấn LINQ để lấy danh sách tất cả các phiếu giảm giá từ DbSet Coupons của _context.
                        select new { c };

            return await query.Select(x => new CouponViewModel() // Chuyển dữ liệu từ các phiếu giảm giá sang dạng ViewModel.
            {
                Id = x.c.Id,
                Code = x.c.Code,
                Count = x.c.Count,
                Promotion = x.c.Promotion,
                Describe = x.c.Describe
            }).ToListAsync(); // Tải danh sách kết quả vào bộ nhớ.
        }

        public async Task<CouponViewModel> GetById(int id) // Phương thức để lấy thông tin của một phiếu giảm giá dựa trên ID.
        {
            var query = from c in _context.Coupons // Tạo một truy vấn LINQ để lấy thông tin của phiếu giảm giá dựa trên ID.
                        where c.Id == id // Lọc danh sách để chỉ lấy phiếu giảm giá có ID trùng khớp với tham số id.
                        select new { c };

            return await query.Select(x => new CouponViewModel() // Chuyển dữ liệu từ phiếu giảm giá sang dạng ViewModel.
            {
                Id = x.c.Id,
                Code = x.c.Code,
                Count = x.c.Count,
                Promotion = x.c.Promotion,
                Describe = x.c.Describe
            }).FirstOrDefaultAsync(); // Chuyển kết quả thành một danh sách và lấy phần tử đầu tiên hoặc mặc định (null) nếu không có phần tử nào.
        }
    }
}
