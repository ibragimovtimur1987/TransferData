using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Packaging.Ionic.Zip;
using TransferData.BLL.Models;
using TransferData.BLL.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransferData.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {

        private readonly ITransferExcelService _transferExcelService;

        public  ExcelController(ITransferExcelService _transferExcelService)
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
                    if (fileExtension == ".xls" || fileExtension == ".xlsx") continue;
                    await _transferExcelService.SaveAsync(file);
                }
            }
            else
            {
                throw new BadReadException("Files Not Found");
            }
        }

        // PUT api/<ExcelController>/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] ExcelRowDto excelRowDto)
        {
            if (excelRowDto==null)
            {
                new BadReadException("Body is empty");
            }
            return _transferExcelService.UpdateAsync(excelRowDto);
        }

        // DELETE api/<ExcelController>/5
        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
           
            if (id == null)
            {
                new BadReadException("id is null");
            }
            Guid guid = new Guid(id);
            return _transferExcelService.DeleteAsync(guid);
        }
    }
}
