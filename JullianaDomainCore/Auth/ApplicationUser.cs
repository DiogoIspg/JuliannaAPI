using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JullianaApi.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string UserApiKey { get; set; }
        public string UserAzureUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}