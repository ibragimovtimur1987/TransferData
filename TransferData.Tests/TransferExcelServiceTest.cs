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
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            var listExcelRoeDto1 = await transferExcelService.GetAsync(DateTime.Now);
            var result = listExcelRoeDto1.ToList();         
        }
        [Fact]
        public async Task Update()
        {

            BLL.Models.ExcelRowDto test2 = new BLL.Models.ExcelRowDto
            {
                Id = new Guid("d65b3fee-f4ef-40c5-58b6-08d81849018a"),
                col1 = "888",
                col12 = "777",
            };
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.UpdateAsync(test2);
        }
        [Fact]
        public async Task Delete()
        {
            Guid i = new Guid("f911da43-c07a-4306-76f5-08d8184901b5");
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.DeleteAsync(i);

        }
    }
}
