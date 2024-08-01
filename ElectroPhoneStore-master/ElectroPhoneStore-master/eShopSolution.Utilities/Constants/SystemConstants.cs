using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.Utilities.Constants // Khai báo namespace cho lớp SystemConstants.
{
    public class SystemConstants // Định nghĩa lớp SystemConstants.
    {
        public const string MainConnectionString = "eShopSolutionDb"; // Chuỗi kết nối chính tới cơ sở dữ liệu.
        public const string CartSession = "CartSession"; // Tên của session lưu trữ thông tin giỏ hàng.

        // Class chứa các key chuẩn cần thiết để thiết lập các cặp key-value dưới dạng chuỗi trong session.
        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId"; // Key cho ngôn ngữ mặc định.
            public const string Token = "Token"; // Key cho token.
            public const string BaseAddress = "BaseAddress"; // Key cho địa chỉ cơ sở.
        }

        // Class chứa các thiết lập liên quan đến sản phẩm.
        public class ProductSettings
        {
            public const int NumberOfFeturedProducts = 4; // Số lượng sản phẩm nổi bật.
            public const int NumberOfLatestProducts = 6; // Số lượng sản phẩm mới nhất.
        }

        // Class chứa các hằng số liên quan đến sản phẩm.
        public class ProductConstants
        {
            public const string NA = "N/A"; // Giá trị chuẩn khi không có thông tin.
        }
    }
}
