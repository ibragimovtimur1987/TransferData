using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferData.BLL.Infrastructure;
using TransferData.BLL.Services.Interface;
using Xunit;

namespace TransferData.Tests
{
    public class TransferExcelServiceTest
    {
        private Utils.DependencyResolverHelpercs _serviceProvider;

        public TransferExcelServiceTest()
        {
            // Конфигурация конвертера типов
            AutomapperConfig.Config();
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Api.Startup>()
                .Build();
            _serviceProvider = new Utils.DependencyResolverHelpercs(webHost);
        }
        [Fact]
        public async Task Upload()
        {
            using (System.IO.Stream file = System.IO.File.OpenRead(@"D:\проекты\тестовые задания\TransferData\TransferData\TransferData.Tests\bin\Debug\netcoreapp3.1\test.xlsx"))
            {

                ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
                await transferExcelService.SaveAsync(file, "test");
            }
        }
        [Fact]
        public async Task Get()
        {
            using (System.IO.Stream file = System.IO.File.OpenRead(@"D:\проекты\тестовые задания\TransferData\TransferData\TransferData.Tests\bin\Debug\netcoreapp3.1\test.xlsx"))
            {

                ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
                var listExcelRoeDto1 = await transferExcelService.GetAsync(DateTime.Now, 1);
                var listExcelRoeDto2 = await transferExcelService.GetAsync(DateTime.Now, 2);


                var result = listExcelRoeDto1.ToList();
            }
        }
    }
}
