using AutoMapper;
using Core.Application.Commands;
using Core.Application.Queries.GetUsers;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserCommand, User>();
        }
    }
}
