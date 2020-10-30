using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteTester.DAL.Models;
using WebSiteTester.BLL.Dtos;

using AutoMapper;

namespace WebSiteTester.BLL.Util
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<TestedSite, TestDto>();
            CreateMap<TestResult, TestResultDto>();
            CreateMap<TestedPage, TestItemDto>();
        }
    }
}
