using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static eShopSolution.Utilities.Constants.SystemConstants;

namespace EmailService
{
    public class EmailService : IEmailService
    {
        // Phương thức này gửi email sử dụng SMTP
        public void Send(string from, string to, string subject, string text)
        {
            // Tạo đối tượng thư
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            // Bạn có thể gửi dạng văn bản thường hoặc HTML, bỏ comment định dạng mong muốn
            // Gửi dạng văn bản thường
            // email.Body = new TextPart(TextFormat.Plain) { Text = text };

            // Gửi dạng HTML
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format(text)
            };

            // Gửi email
            // Kết nối tới máy chủ SMTP (trong trường hợp này, là máy chủ SMTP của Gmail)
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            // Xác thực với máy chủ SMTP bằng cách sử dụng thông tin đăng nhập của Gmail
           // smtp.Authenticate("hytranluan@gmail.com", ""); // Bạn nên cung cấp mật khẩu hợp lệ ở đây
           smtp.Authenticate("cuongdoladola2002@gmail.com", "chmu onnh hffc jewf");
            try
            {
                // Gửi email
                smtp.Send(email);
                // Ngắt kết nối từ máy chủ SMTP sau khi gửi
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ xảy ra trong quá trình gửi
                Console.WriteLine(ex.Message);
            }
        }
    }
}
