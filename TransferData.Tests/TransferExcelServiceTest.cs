using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using System.Collections.Generic;

using Xunit;

namespace TransferData.Tests
{
    public class TransferExcelServiceTest
    {
        private Utils.DependencyResolverHelpercs _serviceProvider;

        public TransferExcelServiceTest()
        {

            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Api.Startup>()
                .Build();
            _serviceProvider = new Utils.DependencyResolverHelpercs(webHost);
        }
        [Fact]
        public void Upload()
        {
            using (System.IO.Stream file = System.IO.File.OpenRead("test.xslx"))
            {

                BLL.Services.Interface.ITransferExcelService transferExcelService = _serviceProvider.GetService<BLL.Services.Interface.ITransferExcelService>();
                transferExcelService.Save(file, "test");
             }
        }
    }
}
