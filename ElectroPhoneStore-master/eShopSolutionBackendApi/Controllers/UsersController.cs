﻿using System;
using System.Threading.Tasks;
using eShopSolution.Application.System.Users;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        /* Dùng FromBody thì lấy từ json đã serialize bên UserApiClient truyền vô được
        còn FromForm thì lấy từ form */
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Authencate(request);

            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }

            // Nếu đăng nhập thành công, tăng số lượt truy cập bằng cách gọi phương thức từ IUserService
            var increaseVisitResult = await _userService.IncreaseDailyVisitorCount();

            // Kiểm tra kết quả tăng số lượt truy cập
            if (!increaseVisitResult.IsSuccessed)
            {
                // Xử lý lỗi khi không tăng được số lượt truy cập (nếu cần)
                _logger.LogError("Failed to increase visit count after successful login.");
            }

            return Ok(result);
        }

        [HttpPost]
        // Cho phép người lạ truy cập
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            var result1 = await _userService.Register(request);
            
            var token = result1.ResultObj;
            var user = await _userService.GetByUserName(request.UserName);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Login", new { token, email = user.ResultObj.Email }, Request.Scheme);
            //var message = await MailUtils.MailUtils.SendGmail("hytranluan@gmail.com", user.ResultObj.Email,
            //                                                  "Link xác nhận email", confirmationLink,
            //                                                  "your_email_here", "your_password_here");

            var email = new EmailService.EmailService();
            email.Send("cuongdoladola2002@gmail.com", user.ResultObj.Email, "XÁC NHẬN TÀI KHOẢN", confirmationLink);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var confirmEmailVm = new ConfirmEmailViewModel()
            {
                token = token,
                email = email
            };

            var result = await _userService.ConfirmEmail(confirmEmailVm);

            return Ok(result.IsSuccessed ? nameof(ConfirmEmail) : "Error");
        }

        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // Đường dẫn ví dụ của GetAllPaging
        // http://localhost/api/users/paging?pageIndex=1&pageSize=10&Keyword='Hy'
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var products = await _userService.GetUsersPaging(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpGet("getByUserName/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            if (!user.IsSuccessed)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpGet("getAllUser")]
        public async Task<IActionResult> GetAll()
        {
            var allUser = await _userService.GetAll();
            return Ok(allUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            var result = await _userService.ChangePassword(model);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel request)
        {
            var result = await _userService.ConfirmEmail(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel request)
        {
            var result = await _userService.ForgotPassword(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel request)
        {
            var result = await _userService.ResetPassword(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}