using HWebAPI.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_MsTest
{
    [TestClass]
    public class TestInitializer
    {
        public static HttpClient TestHttpClient;
        public static Mock<ILoginRepository> MockLoginRepository;

        [AssemblyInitialize]
        public static void InitializeTestServer(TestContext testContext)
        {
            var testServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               // this would cause it to use StartupIntegrationTest class
               // or ConfigureServicesIntegrationTest / ConfigureIntegrationTest
               // methods (if existing)
               // rather than Startup, ConfigureServices and Configure
               .UseEnvironment("IntegrationTest"));

            TestHttpClient = testServer.CreateClient();
        }

        public static void RegisterMockRepositories(IServiceCollection services)
        {
            MockLoginRepository = new Mock<ILoginRepository>();
            services.AddSingleton(MockLoginRepository.Object);

            //add more mock repositories below
        }
    }
}
