using Ninject.Modules;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebSiteTester.DAL.Repositories.Interfaces;
using WebSiteTester.DAL.Repositories;

namespace WebSiteTester.BLL.Util
{
    public class NinjectDataRegistration : NinjectModule
    {
        private string connectionString;
        public NinjectDataRegistration(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<ITestsRepository>().To<TestsRepository>().WithConstructorArgument(connectionString);
        }
    }
}
