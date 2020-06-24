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
                Id = new Guid("b57184a2-bb6c-45a7-99d1-08d818220e53"),
                col1 = "test1222",
                col12 = "test44222",
            };
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.UpdateAsync(test2);
           // await transferExcelService.UpdateAsync(test2);

        }
        [Fact]
        public async Task Delete()
        {
            Guid i = new Guid("b57184a2-bb6c-45a7-99d1-08");
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.DeleteAsync(i);

        }
    }
}
