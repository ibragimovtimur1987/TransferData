using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using OfficeOpenXml.Packaging.Ionic.Zip;
using TransferData.Api.Filters;
using TransferData.BLL.Models;
using TransferData.BLL.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransferData.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class ExcelController : ControllerBase
    {

        private readonly ITransferExcelService _transferExcelService;
        public static NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public  ExcelController(ITransferExcelService transferExcelService)
        {
            _transferExcelService = transferExcelService;
        }
            //// GET api/<ExcelController>/5
        [HttpGet]
        public async Task<IEnumerable<ExcelRowDto>> Get(DateTime? createDate)
        {
            if (createDate == null)
            {
                throw new BadReadException("CreateDate Not Found");
            }
            return await _transferExcelService.GetAsync(createDate.Value);

        }

        // POST api/<ExcelController>
        [HttpPost]
        public async Task Post()
        {
            IFormFileCollection files = Request.Form.Files;
            if (files.Count > 0)
            {
                foreach (IFormFile file in files)
                {
                    string fileExtension =
                                     System.IO.Path.GetExtension(file.FileName);
                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        await _transferExcelService.SaveAsync(file);
                    }
                    else
                    {
                        throw new BadReadException("FileExtension not .xls or .xlsx");
                    }
                }
            }
            else
            {
                Logger.Info("Files Not Found");
                throw new BadReadException("Files Not Found");
            }
        }

        // PUT api/<ExcelController>/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] ExcelRowDto excelRowDto)
        {
            if (excelRowDto==null)
            {
                throw new BadReadException("Body is empty");
            }
            return _transferExcelService.UpdateAsync(excelRowDto);
        }

        // DELETE api/<ExcelController>/5
        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
           
            if (id == null)
            {
                throw new BadReadException("id is null");
            }
            Guid guid = new Guid(id);
            return _transferExcelService.DeleteAsync(guid);
        }
    }
}
