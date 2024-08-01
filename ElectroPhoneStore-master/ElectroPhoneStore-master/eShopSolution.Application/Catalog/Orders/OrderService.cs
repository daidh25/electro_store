using eShopSolution.Data.EF; // Import namespace để sử dụng EShopDbContext để tương tác với cơ sở dữ liệu.
using eShopSolution.Data.Entities; // Import namespace để sử dụng các thực thể (entities) trong cơ sở dữ liệu.
using eShopSolution.ViewModels.Common; // Import namespace của các view model chung để sử dụng các view model liên quan.
using eShopSolution.ViewModels.Utilities.Enums; // Import namespace của các enum utility để sử dụng các enum liên quan.
using eShopSolution.ViewModels.Sales; // Import namespace của các view model bán hàng để sử dụng các view model liên quan.
using Microsoft.AspNetCore.Identity; // Import namespace của Identity để sử dụng UserManager<AppUser>.
using System; // Import namespace cho các kiểu dữ liệu cơ bản và hệ thống.
using System.Collections.Generic; // Import namespace cho các collection.
using System.Threading.Tasks; // Import namespace để sử dụng các công cụ cho việc xử lý bất đồng bộ.
using System.Linq; // Import namespace để sử dụng LINQ để truy vấn dữ liệu.
using Microsoft.EntityFrameworkCore; // Import namespace để sử dụng các lớp hỗ trợ cho Entity Framework Core.

namespace eShopSolution.Application.Catalog.Orders // Khai báo namespace cho lớp OrderService.
{
    public class OrderService : IOrderService // Định nghĩa lớp OrderService, triển khai giao diện IOrderService.
    {
        private readonly EShopDbContext _context; // Trường để lưu trữ đối tượng EShopDbContext cho việc truy cập cơ sở dữ liệu.
        private readonly UserManager<AppUser> _userManager; // Trường để lưu trữ đối tượng UserManager<AppUser>.

        public OrderService(EShopDbContext context, UserManager<AppUser> userManager) // Constructor nhận các tham số là đối tượng EShopDbContext và UserManager<AppUser>.
        {
            _context = context; // Gán giá trị của tham số context cho trường _context.
            _userManager = userManager; // Gán giá trị của tham số userManager cho trường _userManager.
        }

        // Phương thức tạo đơn hàng mới.
        public int Create(CheckoutRequest request)
        {
            var orderDetails = new List<OrderDetail>();

            // Lặp qua danh sách chi tiết đơn hàng trong yêu cầu và thêm vào danh sách orderDetails.
            foreach (var item in request.OrderDetails)
            {
                var product = _context.Products.Find(item.ProductId);

                orderDetails.Add(new OrderDetail()
                {
                    Product = product,
                    Quantity = item.Quantity,
                });

                product.Stock -= item.Quantity; // Giảm số lượng hàng tồn kho sau khi đặt hàng.
            }

            var status = (Data.Enums.OrderStatus)1; // Mặc định trạng thái đơn hàng là 1 (đang chờ xử lý).
            var payment_method = "COD"; // Mặc định phương thức thanh toán là COD (thanh toán khi nhận hàng).

            if (request.PaymentMethod == PaymentMethod.CreditCard) // Nếu phương thức thanh toán là CreditCard.
            {
                payment_method = "Credit Card"; // Đặt phương thức thanh toán là Credit Card.
                status = (Data.Enums.OrderStatus)3; // Cập nhật trạng thái đơn hàng thành 3 (đã thanh toán).
            }

            // Tạo mới một đối tượng Order với thông tin từ request.
            var order = new Order()
            {
                UserId = request?.UserID,
                OrderDate = DateTime.Now,
                OrderDetails = orderDetails,
                Status = status,
                ShipName = request.Name,
                ShipAddress = request.Address,
                ShipPhoneNumber = request.PhoneNumber,
                Total = request.Total,
                PaymentMethod = payment_method,
            };

            if (request.CouponId != 0) // Nếu có mã giảm giá được áp dụng.
            {
                var coupon = _context.Coupons.FirstOrDefault(x => x.Id == request.CouponId);
                coupon.Count -= 1; // Giảm số lượng mã giảm giá còn lại.
                order.CouponId = request.CouponId; // Lưu mã giảm giá được sử dụng vào đơn hàng.
            }

            var result = _context.Orders.Add(order); // Thêm đơn hàng vào cơ sở dữ liệu.
            _context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu.
            return result.Entity.Id; // Trả về ID của đơn hàng mới được tạo.
        }

        // Phương thức cập nhật trạng thái đơn hàng.
        public async Task<ApiResult<bool>> UpdateOrderStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId); // Tìm đơn hàng theo ID.
            var status = (int)order.Status; // Lấy trạng thái hiện tại của đơn hàng.

            switch (status)
            {
                case 1:
                    order.Status = (Data.Enums.OrderStatus)2; // Nếu trạng thái là 1, cập nhật thành 2 (đã xác nhận).
                    break;

                case 2:
                    order.Status = (Data.Enums.OrderStatus)3; // Nếu trạng thái là 2, cập nhật thành 3 (đang giao hàng).
                    break;

                case 3:
                    order.Status = (Data.Enums.OrderStatus)4; // Nếu trạng thái là 3, cập nhật thành 4 (đã giao hàng).
                    break;
            }

            await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu.
            return new ApiSuccessResult<bool>(); // Trả về kết quả thành công.
        }

        // Phương thức hủy trạng thái đơn hàng.
        public async Task<ApiResult<bool>> CancelOrderStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId); // Tìm đơn hàng theo ID.

            order.Status = (Data.Enums.OrderStatus)5; // Cập nhật trạng thái thành 5 (đã hủy).

            await _context.SaveChangesAsync(); // Lưu các thay đổi vào cơ sở dữ liệu.
            return new ApiSuccessResult<bool>(); // Trả về kết quả thành công.
        }

        // Phương thức lấy danh sách đơn hàng phân trang.
        public async Task<PagedResult<OrderViewModel>> GetAllPaging(GetManageOrderPagingRequest request)
        {
            var query = from o in _context.Orders
                        join c in _userManager.Users on o.UserId equals c.Id
                        select new { o, c };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Name.Contains(request.Keyword)
                 || x.o.ShipPhoneNumber.Contains(request.Keyword));
            }

            // Đếm tổng số lượng đơn hàng thỏa mãn điều kiện tìm kiếm.
            int totalRow = await query.CountAsync();

            // Lấy danh sách đơn hàng phân trang.
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new OrderViewModel()
                {
                    Id = x.o.Id,
                    UserID = x.o.UserId,
                    Name = x.c.Name,
                    Address = x.c.Address,
                    PhoneNumber = x.c.PhoneNumber,
                    Email = x.c.Email,
                    OrderDate = x.o.OrderDate,
                    Status = (OrderStatus)x.o.Status,
                    ShipName = x.o.ShipName,
                    ShipAddress = x.o.ShipAddress,
                    ShipPhoneNumber = x.o.ShipPhoneNumber,
                    Price = x.o.Total
                }).ToListAsync();

            // Tạo đối tượng PagedResult chứa thông tin về trang hiện tại và danh sách đơn hàng.
            var pagedResult = new PagedResult<OrderViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult; // Trả về kết quả phân trang.
        }

        // Phương thức lấy danh sách đơn hàng của một người dùng.
        public async Task<OrderByUserViewModel> GetOrderByUser(string userId)
        {
            var Guid = new Guid(userId);

            // Lấy danh sách đơn hàng của người dùng.
            var query = from o in _context.Orders
                        join c in _userManager.Users on o.UserId equals c.Id
                        where c.Id == Guid
                        select new { o, c };

            var data = await query
                 .Select(x => new OrderViewModel()
                 {
                     Id = x.o.Id,
                     UserID = x.o.UserId,
                     Name = x.c.Name,
                     Address = x.c.Address,
                     PhoneNumber = x.c.PhoneNumber,
                     Email = x.c.Email,
                     OrderDate = x.o.OrderDate,
                     Status = (OrderStatus)x.o.Status,
                     ShipName = x.o.ShipName,
                     ShipAddress = x.o.ShipAddress,
                     ShipPhoneNumber = x.o.ShipPhoneNumber,
                     Price = x.o.Total
                 }).ToListAsync();

            var orderList = data.ToList();

            // Lặp qua danh sách đơn hàng để lấy chi tiết đơn hàng (chưa hiện thực).
            foreach (var item in orderList)
            {
                //item.OrderDetails = GetOrderDetails(item.Id);
            }

            // Lấy thông tin người dùng.
            var userInformation = await _userManager.FindByIdAsync(Guid.ToString());

            var userID = Guid;
            var name = userInformation.Name;
            var username = userInformation.UserName;
            var address = userInformation.Address;
            var phoneNumber = userInformation.PhoneNumber;
            var email = userInformation.Email;

            // Tạo đối tượng OrderByUserViewModel chứa thông tin người dùng và đơn hàng.
            var orderByUserVM = new OrderByUserViewModel()
            {
                UserID = userID,
                Name = name,
                UserName = username,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email,
                Orders = orderList,
            };

            return orderByUserVM; // Trả về đối tượng chứa thông tin đơn hàng và người dùng.
        }

        // Phương thức lấy thông tin đơn hàng theo ID.
        public OrderViewModel GetOrderById(int orderId)
        {
            var query = from o in _context.Orders
                        select new { o };

            var orders = query.ToList();

            var order = orders.FirstOrDefault(x => x.o.Id == orderId);

            // Tạo đối tượng OrderViewModel từ đơn hàng và thông tin chi tiết đơn hàng (chưa hiện thực).
            var orderVM = new OrderViewModel()
            {
                Id = order.o.Id,
                UserID = order.o.UserId,
                OrderDate = order.o.OrderDate,
                Status = (OrderStatus)order.o.Status,
                ShipName = order.o.ShipName,
                ShipAddress = order.o.ShipAddress,
                ShipPhoneNumber = order.o.ShipPhoneNumber,
                Price = order.o.Total,
                OrderDetails = GetOrderDetails(order.o.Id)
            };

            return orderVM; // Trả về thông tin đơn hàng.
        }

        // Phương thức lấy danh sách chi tiết đơn hàng.
        public List<OrderDetailViewModel> GetOrderDetails(int orderId)
        {
            var order = _context.OrderDetails.Where(x => x.OrderId == orderId);

            var orderDetails = new List<OrderDetailViewModel>();

            foreach (var item in order)
            {
                orderDetails.Add(new OrderDetailViewModel()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            return orderDetails; // Trả về danh sách chi tiết đơn hàng.
        }

        // Phương thức tính tổng giá của đơn hàng.
        public decimal GetOrderPrice(List<OrderDetailViewModel> orderDetails)
        {
            decimal price = 0;
            foreach (var item in orderDetails)
            {
                var product = _context.Products.Find(item.ProductId);
                price += product.Price * item.Quantity;
            }

            return price; // Trả về tổng giá của đơn hàng.
        }
    }
}
