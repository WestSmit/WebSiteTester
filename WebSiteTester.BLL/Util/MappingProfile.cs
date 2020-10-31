using WebSiteTester.DAL.Entities;
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
