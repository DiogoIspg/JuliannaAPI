using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JullianaApi.Auth;
using JullianaDomainCore.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JullianaApi.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("users/authenticate")]
        public async Task<object> GetToken([FromBody] UserPass userPass)
        {
            if (userPass.Username == null || userPass.Password == null)
                return this.BadRequest(new { Error = "Invalid credentials" });

            var result = await this.signInManager.PasswordSignInAsync(userPass.Username, userPass.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await this.userManager.FindByNameAsync(userPass.Username);
                return new
                {
                    Username = userPass.Username,
                    UserId = appUser.Id,
                    Token = this.GenerateJwtToken(appUser)
                };
            }
            else
            {
                return this.BadRequest(new
                {
                    Error = "Invalid credentials"
                });
            }
        }

        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserPass userPass)
        {
            var currentUser = new ApplicationUser
            {
                UserName = userPass.Username,
                Name = userPass.Username,
                Email = userPass.Email,
                IsDeleted = false,
                LockoutEnabled = false,
                LockoutEnd = null,
            };

            // create user
            var createResult = await this.userManager.CreateAsync(currentUser, userPass.Password);

            if (!createResult.Succeeded)
                return this.BadRequest(new
                {
                    Error = string.Join(";\n", createResult.Errors)
                });

            return this.Ok();
        }

        private async Task<object> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await this.userManager.GetRolesAsync(user);

            var claims = new List<Claim>();
            // dummy permission claim to force permission claim to be an array
            // otherwise generates "Permissions: permissionName" instead of
            // "Permissions: [ permissionName ]"
            claims.Add(new Claim(PermissionsPolicies.PermissionsClaimName, "dummy"));

            foreach (var role in userRoles)
            {
                var identityRole = await this.roleManager.FindByNameAsync(role);
                claims.AddRange(await this.roleManager.GetClaimsAsync(identityRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.configuration["JwtExpireDays"], CultureInfo.InvariantCulture));

            var token = new JwtSecurityToken(
                this.configuration["JwtIssuer"],
                this.configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> GetClaims(ApplicationUser user)
        {
            var userClaims = new List<Claim>();

            // TODO: logic to get claims
            return userClaims;
        }

        public class UserPass
        {
            public string Username { get; set; }

            public string Password { get; set; }

            public string Email { get; set; }
        }
    }
}