using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TransferData.BLL.DTO;

namespace TransferData.BLL.Services.Interface
{
    public interface ITransferExcelService
    {
       Task Save(IFormFile excelModelForm);

       Task Save(Stream fs, string fileName);
    }
}
