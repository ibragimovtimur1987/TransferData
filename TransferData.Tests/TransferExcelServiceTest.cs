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
                var listExcelRoeDto1 = await transferExcelService.GetAsync(DateTime.Now, 1);
                var listExcelRoeDto2 = await transferExcelService.GetAsync(DateTime.Now, 2);


                var result = listExcelRoeDto1.ToList();         
        }
        [Fact]
        public async Task Update()
        {
            BLL.Models.ExcelRowDto test1 = new BLL.Models.ExcelRowDto
            {
                Id = 5,
                col1 = "test1",
                col12 = "test44",
                sheetId = 1
            };
            BLL.Models.ExcelRowDto test2 = new BLL.Models.ExcelRowDto
            {
                Id = 2,
                col1 = "test1222",
                col12 = "test44222",
                sheetId = 2
            };
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.UpdateAsync(test1);
            await transferExcelService.UpdateAsync(test2);

        }
        [Fact]
        public async Task Delete()
        {
            int i = 3;
            ITransferExcelService transferExcelService = _serviceProvider.GetService<ITransferExcelService>();
            await transferExcelService.DeleteAsync(i);

        }
    }
}
