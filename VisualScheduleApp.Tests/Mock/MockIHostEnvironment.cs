using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace VisualScheduleApp.Tests.Mock
{
    public class MockIHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";
        public string ApplicationName { get; set; } = "VisualScheduleApp";

        public string ContentRootPath { get; set; } =
            Path.GetTempPath(); 

        public IFileProvider ContentRootFileProvider { get; set; } =
            new PhysicalFileProvider(Path.GetTempPath());
    }
}