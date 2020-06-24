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
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        /// <summary>
        /// Репозиторий маркетинговых акций
        /// </summary>
        private readonly ITransferExcelService _excelConverterService;

        // GET: api/<ExcelController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExcelController>/5
        [HttpGet("{createDate}")]
        public async Task<IEnumerable<ExcelRowDto>> Get(DateTime? createDate,int? sheetId)
        {
            if (createDate == null || sheetId == null)
            {
                throw new BadReadException("CreateDate or SheetId Not Found");
            }
            return await _excelConverterService.GetAsync(createDate.Value, sheetId.Value);
                        
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
                    await _excelConverterService.SaveAsync(file);
                }
            }
        }

        // PUT api/<ExcelController>/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] ExcelRowDto excelRowDto)
        {
            excelRowDto.sheetId = id;
            return _excelConverterService.UpdateAsync(excelRowDto);

        }

        // DELETE api/<ExcelController>/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _excelConverterService.DeleteAsync(id);
        }
    }
}
