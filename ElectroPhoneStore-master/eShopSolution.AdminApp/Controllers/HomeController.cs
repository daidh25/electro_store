using eShopSolution.AdminApp.Models;
using eShopSolution.ApiIntegration;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    // Authorize: Sẽ chuyển sang trang User/Login ( định nghĩa trong startup bằng serivces.AddAuthorization )
    // Sau đó phải đăng nhập rồi mới được dùng mấy trang này
    //[Authorize]
    public class HomeController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderApiClient _orderApiClient;

        public HomeController(IProductApiClient productApiClient,
            IConfiguration configuration, ILogger<HomeController> logger,
            IUserApiClient userApiClient, IOrderApiClient orderApiClient
            )
        {
            _orderApiClient = orderApiClient;
            _userApiClient = userApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
            _logger = logger;
        }
   
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
        {
              

            
            // lấy user name đăng nhập
            var user = User.Identity.Name;
            var expireTime = HttpContext.Items["ExpiresUTC"];

            //
            var products = await _productApiClient.GetAll();
        


            var stockByCategory = await _productApiClient.GetStockByCategory();
            var distinctProductIds = products.Select(p => p.Id).Distinct();
            
            var totalStock = distinctProductIds.Count();
            ViewBag.TotalStock = totalStock;
            ViewBag.Products = products;
            ViewBag.StockByCategory = stockByCategory;
            //
            var totaluser = await _userApiClient.GetAll();
            var countusers = totaluser.Select(p => p.Id).Count();
            ViewBag.countusers = countusers;
            //
            var totaloder = await _orderApiClient.GetAll();
            var countoders = totaloder.Select(o => o.Id).Count();
            ViewBag.countoders = countoders;
            var totalrevenue = totaloder.Where(o => o.Status != OrderStatus.Canceled).Sum(o => o.Price).ToString("#,##0.##");
            ViewBag.totalrevenue = totalrevenue;



            var orders = await _orderApiClient.GetAll();
   
            // Lọc các đơn hàng trong khoảng thời gian đã chọn
            var filteredOrders = orders
                .Where(o => o.OrderDate.Date >= startDate.Date && o.OrderDate.Date < endDate.Date.AddDays(1))
                .ToList();

            // Tạo một Dictionary để lưu tổng giá trị theo từng ngày
            Dictionary<DateTime, decimal> dailyTotalRevenue = new Dictionary<DateTime, decimal>();

            // Lặp qua mỗi ngày trong khoảng thời gian đã chọn
            for (DateTime date = startDate.Date; date < endDate.Date.AddDays(1); date = date.AddDays(1))
            {
                // Tính tổng doanh thu của ngày hiện tại
                decimal totalRevenueOfDay = filteredOrders
                      .Where(o => o.OrderDate.Date == date && o.Status != OrderStatus.Canceled)
                    .Sum(o => o.Price);
                // Lưu tổng doanh thu của ngày hiện tại vào Dictionary
                dailyTotalRevenue[date] = totalRevenueOfDay;
            }

            // Chuyển Dictionary thành chuỗi JSON
            string dailyTotalRevenueJson = JsonConvert.SerializeObject(dailyTotalRevenue);

            // Lưu chuỗi JSON vào Session
            HttpContext.Session.SetString("DailyTotalRevenue", dailyTotalRevenueJson);


            // Lưu ngày bắt đầu vào Session
            HttpContext.Session.SetString("startDay", startDate.Day.ToString());
HttpContext.Session.SetString("startMonth", startDate.Month.ToString());



            //Doanh thu theo khoảng thời gian 
            // Lấy dữ liệu từ session


            var TotalRevenue1 = HttpContext.Session.GetString("DailyTotalRevenue");


            string input = TotalRevenue1;
            input = input.Replace("{", "").Replace("}", "");

            var dateValuePairs = new List<Tuple<string, string>>();

            // Tách chuỗi thành các cặp ngày và giá trị
            string[] pairs = input.Split(',');

            foreach (string pair in pairs)
            {
                // Tách cặp ngày và giá trị từ chuỗi
                string[] parts = pair.Split(':');

                // Lấy ngày và giá trị từ các phần được tách ra
                int indexT = pair.IndexOf('T');
                if (indexT != -1)
                {
                    string date1 = pair.Substring(0, indexT);
                    string date = date1.TrimStart('"');
                    string value = parts[3];

                    // Thêm cặp ngày và giá trị vào danh sách
                    dateValuePairs.Add(new Tuple<string, string>(date, value));
                }
            }

            // Đưa danh sách vào ViewBag
            ViewBag.DateValuePairs = dateValuePairs;

            // Sử dụng dữ liệu từ session ở đây


            ViewBag.TotalRevenue1 = TotalRevenue1;







            //
            return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        // Trang error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    

        [HttpPost]
        public IActionResult Language(NavigationViewModel viewModel)
        {
            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId,
                viewModel.CurrentLanguageId);

            return Redirect(viewModel.ReturnUrl);
        }
    }
}