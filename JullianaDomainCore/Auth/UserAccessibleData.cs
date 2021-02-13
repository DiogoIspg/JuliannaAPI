using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore.Auth
{
    public class UserAccessibleData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserApiKey { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}