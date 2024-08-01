using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class DailyVisitorCountConfiguration : IEntityTypeConfiguration<DailyVisitorCount>
    {
        public void Configure(EntityTypeBuilder<DailyVisitorCount> builder)
        {
            // Đặt tên bảng trong cơ sở dữ liệu là "DailyVisitorCounts"
            builder.ToTable("DailyVisitorCounts");

            // Xác định thuộc tính "Id" là khóa chính của bảng và sử dụng Identity để tự động tăng giá trị
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            // Thiết lập thuộc tính "Date" là bắt buộc
            builder.Property(x => x.Date).IsRequired();

            // Thiết lập thuộc tính "Count" là bắt buộc
            builder.Property(x => x.Count).IsRequired();
        }
    }
}
