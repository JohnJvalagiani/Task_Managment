using AutoMapper;
using Domain.Entities;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace service.server.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<AppUser, UserRead>();
            CreateMap<UserRead, AppUser>();

            CreateMap<AppUser, UserWrite>();
            CreateMap<UserWrite, AppUser>();


            CreateMap<TaskRead, task>().ReverseMap();
            CreateMap<TaskWrite, task>().ReverseMap();



        }


    }
}

