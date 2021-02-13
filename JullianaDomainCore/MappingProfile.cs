using AutoMapper;
using JullianaApi.Auth;
using JullianaDomainCore.Auth;
using JullianaDomainCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JullianaDomainCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserAccessibleData>();
        }
    }
}