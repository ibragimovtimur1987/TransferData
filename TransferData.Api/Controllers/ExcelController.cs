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
        /// <summary>
        /// Репозиторий маркетинговых акций
        /// </summary>
        private readonly ITransferExcelService _excelConverterService;

        //// GET: api/<ExcelController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public ExcelController(ITransferExcelService excelConverterService)
        {
            _excelConverterService = excelConverterService;
        }
            // GET api/<ExcelController>/5
        [HttpGet]
        public async Task<IEnumerable<ExcelRowDto>> Get(DateTime? createDate)
        {
            if (createDate == null)
            {
                throw new BadReadException("CreateDate or SheetId Not Found");
            }
            return await _excelConverterService.GetAsync(createDate.Value);
                        
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
            else
            {
                throw new BadReadException("Files Not Found");
            }
        }

        // PUT api/<ExcelController>/5
        [HttpPut("{id}")]
        public Task Put(Guid id, [FromBody] ExcelRowDto excelRowDto)
        {
            if (excelRowDto==null)
            {
                new BadReadException("Body is empty");
            }
            return _excelConverterService.UpdateAsync(id,excelRowDto);
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
            return _excelConverterService.DeleteAsync(guid);
        }
    }
}
