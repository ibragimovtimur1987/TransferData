using System;
using System.Collections.Generic;
using System.Text;
using TransferData.BLL.DTO;
using TransferData.BLL.Infrastructure;
using TransferData.DAL.Models;

namespace TransferData.BLL.Services.Interface
{
    public interface IExcelProvider
    {
         IEnumerable<ExcelCommonModel> GetTypeExcelTable(ExcelSheetDto excelSheetDto, IAutoMapper _autoMapper);
    }
}
