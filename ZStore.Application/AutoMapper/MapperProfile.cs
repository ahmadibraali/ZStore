using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZStore.Core;
using ZStore.DTO;

namespace ZStore.Application.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomUser, RegisteredUserViewModel>().ReverseMap();

        }
    }
}
