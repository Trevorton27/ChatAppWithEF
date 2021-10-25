using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ChatAppWithEF.DTOs;
using ChatAppWithEF.Models;

namespace ChatAppWithEF.Services
{
    public class AutoMapper : Profile

    {
        public AutoMapper()
        {
            CreateMap<User, RegisterDTO>();
        }
    }
}
