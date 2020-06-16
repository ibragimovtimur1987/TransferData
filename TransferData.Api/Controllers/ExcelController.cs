using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IExcelConverterService _excelConverterService;

        // GET: api/<ExcelController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExcelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
                   await _excelConverterService.Save(file);
                }
            }
        }

        // PUT api/<ExcelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExcelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
