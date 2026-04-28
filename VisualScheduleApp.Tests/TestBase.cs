using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VisualScheduleApp.ApplicationServices.Services;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;
using VisualScheduleApp.Tests.Macros;
using VisualScheduleApp.Tests.Mock;

namespace VisualScheduleApp.Tests
{
    public abstract class TestBase
    {
        protected IServiceProvider serviceProvider { get; set; }

        protected TestBase()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IChildServices, ChildServices>();
            services.AddScoped<IActivityServices, ActivityServices>();
            services.AddScoped<IScheduleServices, ScheduleServices>();
            services.AddScoped<IScheduleItemServices, ScheduleItemServices>();
            services.AddScoped<IFileServices, FileServices>();

            services.AddScoped<IHostEnvironment, MockIHostEnvironment>();

            services.AddDbContext<VisualScheduleAppContext>(x =>
            {
                x.UseInMemoryDatabase(Guid.NewGuid().ToString());
                x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            RegisterMacros(services);
        }

        protected T? Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }

        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(t => macroBaseType.IsAssignableFrom(t)
                            && !t.IsInterface
                            && !t.IsAbstract);

            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }
        }
    }
}