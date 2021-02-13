using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JullianaDomainCore.Auth
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : IdentityUser
    {
        private const string ERROR_LENGHT_MAX = "Password cannot be more than 20 caracters long.";

        private const int MAX_PASSWORD_LENGTH = 20;

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            if (password.Length > MAX_PASSWORD_LENGTH)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "Password",
                    Description = ERROR_LENGHT_MAX
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}