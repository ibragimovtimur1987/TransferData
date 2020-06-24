using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TransferData.BLL.DTO;
using TransferData.BLL.Models;

namespace TransferData.BLL.Services.Interface
{
    public interface ITransferExcelService
    {
       Task SaveAsync(IFormFile excelModelForm);

       Task SaveAsync(Stream fs, string fileName);

       Task<IEnumerable<ExcelRowDto>> GetAsync(DateTime createDateTime, int sheetId);
    }
}
