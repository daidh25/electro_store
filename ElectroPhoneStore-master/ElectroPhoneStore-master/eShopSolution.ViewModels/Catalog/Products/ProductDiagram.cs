using eShopSolution.Utilities.Constants; // Import namespace của các hằng số trong ứng dụng để sử dụng các hằng số liên quan.
using eShopSolution.ViewModels.Catalog.Categories; // Import namespace của các view model danh mục để sử dụng các view model liên quan.
using Microsoft.AspNetCore.Http; // Import namespace của Microsoft.AspNetCore.Http để sử dụng các thành phần liên quan đến HTTP trong ASP.NET Core.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.ComponentModel.DataAnnotations; // Import namespace để sử dụng các annotation để xác định metadata của các thuộc tính.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class ProductDiagram
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
