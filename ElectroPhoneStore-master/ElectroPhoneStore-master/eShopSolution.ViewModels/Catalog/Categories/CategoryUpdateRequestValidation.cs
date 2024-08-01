using FluentValidation; // Import namespace của FluentValidation để thực hiện validation.
using System; // Import namespace System để sử dụng các thành phần cơ bản của .NET.
using System.Collections.Generic; // Import namespace cần thiết để sử dụng các collection.
using System.Text; // Import namespace để làm việc với chuỗi và encoding.

namespace eShopSolution.ViewModels.Catalog.Categories // Khai báo namespace cho lớp CategoryUpdateRequestValidation.
{
    public class CategoryUpdateRequestValidation : AbstractValidator<CategoryCreateRequest> // Định nghĩa lớp CategoryUpdateRequestValidation kế thừa từ AbstractValidator<CategoryCreateRequest>.
    {
        public CategoryUpdateRequestValidation() // Constructor của lớp CategoryUpdateRequestValidation.
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên danh mục không được để trống") // Quy tắc: Tên danh mục không được để trống.
                .MaximumLength(200).WithMessage("Tên danh mục không được vượt quá 200 kí tự"); // Quy tắc: Tên danh mục không được vượt quá 200 kí tự.
        }
    }
}
