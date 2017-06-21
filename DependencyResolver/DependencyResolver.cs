using BLL.Interface.Interfaces;
using BLL.Services;
using DAL.Concrete;
using DAL.Concrete.Repositories;
using DAL.Interface.Interfaces;
using Logger.Interfaces;
using Logger;
using Ninject;
using Ninject.Web.Common;
using ORM;
using System.Data.Entity;


namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel);
        }

        private static void Configure(IKernel kernel)
        {

            kernel.Bind<DbContext>().To<TestEntities>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<ILogger>().To<NLoggerAdapter>().InSingletonScope();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();

            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<ITestRepository>().To<TestRepository>();

            kernel.Bind<ITestResultService>().To<TestResultService>();
            kernel.Bind<ITestResultRepository>().To<TestResultRepository>();

            kernel.Bind<IQuestionService>().To<QuestionService>();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>();

            kernel.Bind<IAnswerService>().To<AnswerService>();
            kernel.Bind<IAnswerRepository>().To<AnswerRepository>();

            kernel.Bind<IThemeService>().To<ThemeService>();
            kernel.Bind<IThemeRepository>().To<ThemeRepository>();

        }
    }
}
