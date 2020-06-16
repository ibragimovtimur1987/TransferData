using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.BLL.DTO;

namespace TransferData.BLL.Services.Interface
{
    public interface IExcelConverterService
    {
       Task Save(IFormFile excelModelForm);
    }
}
