using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Common // Khai báo namespace cho lớp ApiErrorResult.
{
    public class ApiErrorResult<T> : ApiResult<T> // Định nghĩa lớp ApiErrorResult, kế thừa từ lớp cơ sở ApiResult với kiểu dữ liệu generic T.
    {
        // Biến chứa 1 list các error trong quá trình ta validation
        // Do là 1 list error nên phải là một mảng string
        public string[] ValidationErrors { get; set; } // Thuộc tính để lưu trữ danh sách các lỗi validation.

        public ApiErrorResult() // Constructor mặc định không có tham số.
        {
        }

        public ApiErrorResult(string message) // Constructor với tham số message.
        {
            IsSuccessed = false; // Đặt IsSuccessed là false để biểu thị rằng có lỗi xảy ra.
            Message = message; // Gán giá trị message cho thuộc tính Message.
        }

        public ApiErrorResult(string[] validationErrors) // Constructor với tham số validationErrors là danh sách các lỗi validation.
        {
            IsSuccessed = false; // Đặt IsSuccessed là false để biểu thị rằng có lỗi xảy ra.
            ValidationErrors = validationErrors; // Gán giá trị validationErrors cho thuộc tính ValidationErrors.
        }
    }
}
