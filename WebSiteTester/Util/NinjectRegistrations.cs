using Ninject.Modules;
using WebSiteTester.BLL.Services.Interfaces;
using WebSiteTester.BLL.Services;
using WebSiteTester.BLL.Util;
using AutoMapper;
using Ninject;

namespace WebSiteTester.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ISitemapProvider>().To<SitemapProvider>();
            Bind<IResponseTimeTester>().To<ResponseTimeTester>();
            Bind<ITesterService>().To<TesterService>();

            var mapperConfiguration = new MapperConfiguration(c => {
                c.AddProfile(new MappingProfile());
            });
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }
    }
}