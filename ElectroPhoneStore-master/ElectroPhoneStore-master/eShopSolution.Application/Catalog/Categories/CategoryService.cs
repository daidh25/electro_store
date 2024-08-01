using eShopSolution.Data.EF; // Import namespace để sử dụng EShopDbContext để tương tác với cơ sở dữ liệu.
using eShopSolution.ViewModels.Catalog.Categories; // Import namespace của các view model danh mục để sử dụng các view model liên quan.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Linq; // Import namespace để sử dụng LINQ để truy vấn dữ liệu.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.
using Microsoft.EntityFrameworkCore; // Import namespace để sử dụng các lớp hỗ trợ cho Entity Framework Core.
using eShopSolution.Data.Entities; // Import namespace để sử dụng các thực thể (entities) trong cơ sở dữ liệu.
using eShopSolution.Utilities.Exceptions; // Import namespace để sử dụng các lớp xử lý ngoại lệ tùy chỉnh.
using eShopSolution.ViewModels.Catalog.Products; // Import namespace của các view model sản phẩm để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Common; // Import namespace của các view model chung để sử dụng các view model liên quan.

namespace eShopSolution.Application.Catalog.Categories // Khai báo namespace cho lớp CategoryService.
{
    public class CategoryService : ICategoryService // Định nghĩa lớp CategoryService, triển khai giao diện ICategoryService.
    {
        private readonly EShopDbContext _context; // Trường để lưu trữ đối tượng EShopDbContext cho việc truy cập cơ sở dữ liệu.

        public CategoryService(EShopDbContext context) // Constructor nhận một tham số là đối tượng EShopDbContext.
        {
            _context = context; // Gán giá trị của tham số context cho trường _context.
        }

        public async Task<int> Create(CategoryCreateRequest request) // Phương thức để tạo mới một danh mục sản phẩm.
        {
            var category = new Category() // Tạo mới một đối tượng Category từ dữ liệu trong request.
            {
                Name = request.Name // Gán tên danh mục từ dữ liệu trong request.
            };

            _context.Categories.Add(category); // Thêm category vào DbSet Categories của _context.
            await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu.
            return category.Id; // Trả về ID của danh mục vừa được tạo.
        }

        public async Task<int> Update(CategoryUpdateRequest request) // Phương thức để cập nhật thông tin của một danh mục sản phẩm.
        {
            var category = await _context.Categories.FindAsync(request.Id); // Tìm danh mục cần cập nhật dựa trên ID từ request.
            if (category == null) throw new EShopException($"Không thể tìm danh mục có ID: {request.Id} "); // Nếu không tìm thấy danh mục, ném một ngoại lệ EShopException.

            category.Name = request.Name; // Cập nhật tên danh mục từ dữ liệu trong request.

            return await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu và trả về số lượng bản ghi đã được cập nhật.
        }

        public async Task<int> Delete(int categoryId) // Phương thức để xóa một danh mục sản phẩm.
        {
            var category = await _context.Categories.FindAsync(categoryId); // Tìm danh mục cần xóa dựa trên ID.
            if (category == null) throw new EShopException($"Không thể tìm danh mục có ID: {categoryId} "); // Nếu không tìm thấy danh mục, ném một ngoại lệ EShopException.

            _context.Categories.Remove(category); // Xóa danh mục khỏi DbSet Categories của _context.

            return await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu và trả về số lượng bản ghi đã được xóa.
        }

        public async Task<PagedResult<CategoryViewModel>> GetAllPaging(GetManageProductPagingRequest request) // Phương thức để lấy danh sách các danh mục sản phẩm theo trang.
        {
            var query = from c in _context.Categories // Tạo một truy vấn LINQ để lấy danh sách các danh mục từ DbSet Categories của _context.
                        select new { c };

            if (!string.IsNullOrEmpty(request.Keyword)) // Nếu có từ khóa tìm kiếm.
                query = query.Where(x => x.c.Name.Contains(request.Keyword)); // Lọc danh sách các danh mục dựa trên từ khóa.

            // Phân trang
            int totalRow = await query.CountAsync(); // Đếm tổng số danh mục.

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize) // Bỏ qua các bản ghi không cần thiết.
                .Take(request.PageSize) // Chọn số lượng bản ghi trên mỗi trang.
                .Select(x => new CategoryViewModel() // Chuyển dữ liệu từ danh mục thành CategoryViewModel.
                {
                    Id = x.c.Id,
                    Name = x.c.Name,
                }).ToListAsync(); // Chuyển kết quả thành một danh sách và tải vào bộ nhớ.

            // Tạo kết quả phân trang
            var pagedResult = new PagedResult<CategoryViewModel>()
            {
                TotalRecords = totalRow, // Tổng số bản ghi.
                PageSize = request.PageSize, // Số lượng bản ghi trên mỗi trang.
                PageIndex = request.PageIndex, // Trang hiện tại.
                Items = data // Danh sách các danh mục trên trang hiện tại.
            };
            return pagedResult; // Trả về kết quả phân trang.
        }

        public async Task<List<CategoryViewModel>> GetAll() // Phương thức để lấy danh sách tất cả các danh mục sản phẩm.
        {
            var query = from c in _context.Categories // Tạo một truy vấn LINQ để lấy danh sách các danh mục từ DbSet Categories của _context.
                        select new { c };

            return await query.Select(x => new CategoryViewModel() // Chuyển dữ liệu từ danh mục thành CategoryViewModel.
            {
                Id = x.c.Id,
                Name = x.c.Name,
            }).ToListAsync(); // Chuyển kết quả thành một danh sách và tải vào bộ nhớ.
        }

        public async Task<CategoryViewModel> GetById(int id) // Phương thức để lấy thông tin của một danh mục sản phẩm dựa trên ID.
        {
            var query = from c in _context.Categories // Tạo một truy vấn LINQ để lấy danh sách các danh mục từ DbSet Categories của _context.
                        where c.Id == id // Lọc danh sách để chỉ lấy danh mục có ID trùng khớp với tham số id.
                        select new { c };

            return await query.Select(x => new CategoryViewModel() // Chuyển dữ liệu từ danh mục thành CategoryViewModel.
            {
                Id = x.c.Id,
                Name = x.c.Name,
            }).FirstOrDefaultAsync(); // Chuyển kết quả thành một danh sách và lấy phần tử đầu tiên hoặc mặc định (null) nếu không có phần tử nào.
        }
    }
}
