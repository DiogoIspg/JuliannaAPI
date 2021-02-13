using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JullianaApi.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JullianaDomainCore.Auth;
using Microsoft.AspNetCore.Authorization;

namespace JullianaApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [ApiController]
    public class UserController : Controller
    {
        //private readonly IDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        private IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet()]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            var users = this.mapper.Map<List<UserAccessibleData>>(this.userManager.Users);
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var currentUser = await this.userManager.FindByIdAsync(userId);

            var accessibleUserData = this.mapper.Map<UserAccessibleData>(currentUser);

            return Ok(accessibleUserData);
        }

        [HttpPut("{userId}/updatePassword")]
        public async Task<IActionResult> UpdateUserPassword(string userId, NewPasswordRequest newPassword)
        {
            var currentUser = await this.userManager.FindByIdAsync(userId);

            currentUser.PasswordHash = this.userManager.PasswordHasher.HashPassword(currentUser, newPassword.NewPassword);

            await this.userManager.UpdateAsync(currentUser);

            return Ok(currentUser);
        }

        [HttpPut("{userId}/updateEmail")]
        public async Task<IActionResult> UpdateUserEmail(string userId, NewEmailRequest newEmail)
        {
            var currentUser = await this.userManager.FindByIdAsync(userId);

            currentUser.Email = newEmail.NewEmail;

            await this.userManager.UpdateAsync(currentUser);

            return Ok(currentUser);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var currentUser = await this.userManager.FindByIdAsync(userId);

            currentUser.IsDeleted = true;

            await this.userManager.UpdateAsync(currentUser);

            return Ok(currentUser);
        }

        public class NewEmailRequest
        {
            public string NewEmail { get; set; }
        }

        public class NewPasswordRequest
        {
            public string NewPassword { get; set; }
        }
    }
}