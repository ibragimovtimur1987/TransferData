using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Q101.ExcelLoader.Abstract;
using Q101.ExcelLoader.Concrete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferData.BLL.DTO;
using TransferData.BLL.Models;
using TransferData.BLL.Services.Interface;
using TransferData.BLL.Infrastructure;
using TransferData.DAL.Models;
using TransferData.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace TransferData.BLL.Services
{
    public class TransferExcelService: ITransferExcelService
    {
        /// <summary>
        /// Загрузчик Excel файлов.
        /// </summary>
        private readonly IExcelFileLoader _excelLoader;
        /// <summary>
        /// Excel Repository 1
        /// </summary>
        private readonly IGenericRepository<ExcelModel1> _excel1Repository;
        /// <summary>
        /// Excel Repository 2
        /// </summary>
        private readonly IGenericRepository<ExcelModel2> _excel2Repository;
        /// <summary>
        /// Конвертер типов
        /// </summary>
        ///         
        private readonly IAutoMapper _autoMapper;
        /// Логирование.
        /// </summary>
        private NLog.Logger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public TransferExcelService(IExcelFileLoader excelLoader, IGenericRepository<ExcelModel1> excelRepository, IGenericRepository<ExcelModel2> excel2Repository, IAutoMapper autoMapper)
        {
            _excelLoader = excelLoader;
            _excel1Repository = excelRepository;
            _excel2Repository = excel2Repository;
            _autoMapper = autoMapper;
        }
        public async Task SaveAsync(IFormFile excelModelForm)
        {
            using (var fs = excelModelForm.OpenReadStream())
            {
               await SaveAsync(fs, excelModelForm.FileName);
            }
        }
        public async Task SaveAsync(Stream fs, string fileName)
        {
          
            List<ExcelSheetDto> listExcelSheetDto = Convert(fs, fileName);
           
            foreach (ExcelSheetDto excelSheetDto in listExcelSheetDto)
            {
                if (excelSheetDto.SheetId == 1)
                {
                    var listExcel = excelSheetDto.ExcelListRowDto.ToList();
                    var exelModels = listExcel.Select(_autoMapper.Map<ExcelModel1>);
                    if (exelModels.Count() > 0)await _excel1Repository.SaveAsync(exelModels);
                   
                }
                else if (excelSheetDto.SheetId == 2)
                {
                    var listExcel = excelSheetDto.ExcelListRowDto.ToList();
                    var exelModels = listExcel.Select(_autoMapper.Map<ExcelModel2>);
                    if (exelModels.Count() > 0) await _excel2Repository.SaveAsync(exelModels);
                }
            }
        }
        public async Task<IEnumerable<ExcelRowDto>> GetAsync(DateTime createDateTime)
        {
                var excelCommonModel1 = await _excel1Repository.FindByAsyn(x => x.CreatedDate != null && x.CreatedDate.Date == createDateTime.Date);
                var exelModels1 = excelCommonModel1.Select(_autoMapper.Map<ExcelRowDto>);

               var excelCommonModel2 = await _excel2Repository.FindByAsyn(x => x.CreatedDate != null && x.CreatedDate.Date == createDateTime.Date);
               var exelModels2 = excelCommonModel2.Select(_autoMapper.Map<ExcelRowDto>);

               return exelModels1.Concat(exelModels2);
        }
        public async Task DeleteAsync(Guid Id)
        {
            ExcelModel1 excelModel1 = await _excel1Repository.FindAsync(x => x.Id == Id);
            if (excelModel1 != null)
            {
                await _excel1Repository.DeleteAsyn(excelModel1);
            }
            else
            {
                ExcelModel2 excelModel2 = await _excel2Repository.FindAsync(x => x.Id == Id);
                if (excelModel2 != null)
                {
                    await _excel2Repository.DeleteAsyn(excelModel2);
                }
                else
                {
                    throw new Exception("Id not found in Excel Tables");
                }
            }
        }
        public async Task UpdateAsync(ExcelRowDto excelRowDto)
        {
            excelRowDto.ModifiedDate = DateTime.Now;
            if (excelRowDto != null)
            {              
                if (_excel1Repository.AnyAsync(x => x.Id == excelRowDto.Id).Result)
                {
                    var excelModel1 = _autoMapper.Map<ExcelModel1>(excelRowDto);                 
                    await _excel1Repository.UpdateAsyn(excelModel1, excelRowDto.Id);
                }
                else if (_excel2Repository.AnyAsync(x => x.Id == excelRowDto.Id).Result)
                {
                    var excelModel2 = _autoMapper.Map<ExcelModel2>(excelRowDto);
                    await _excel2Repository.UpdateAsyn(excelModel2, excelRowDto.Id);
                }
            }
            else
            {
                throw new Exception("excelRowDto is null");
            }
        }
        /// <inheritdoc />
        private List<ExcelSheetDto> Convert(Stream fs, string fileName)
        {         
            var excelFile = _excelLoader.Load(fs);
            int i = 1;
            var listExcelSheetDto = new List<ExcelSheetDto>();
            if (excelFile == null)
            {
                throw new Exception("excelFile is null");
            }
            if (excelFile.Sheets.Count() == 0)
            {
                throw new Exception("not found excel sheet");
            }
            foreach (var sheet in excelFile.Sheets)
            {
                var excelFileContentDto = new ExcelSheetDto
                {
                    SheetId = i,
                    ExcelListRowDto = GetExcelModelDto(sheet, fileName)
                };
                listExcelSheetDto.Add(excelFileContentDto);
                i++;
            }
            return listExcelSheetDto;         
        }
        /// <summary>
        /// Получить список excel dto по sheet.
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="excelModel">Модель файла.</param>
        private List<ExcelRowDto> GetExcelModelDto(ExcelSheetModel excelSheetModel,string fileName)
        {
            var listExcelModelDto = new List<ExcelRowDto>();
            if (excelSheetModel == null || excelSheetModel.Rows == null)
            {
                return listExcelModelDto;
            }
            var rows = excelSheetModel.Rows.ToList();
            for (int i = 1; i < rows.Count; i++)
            {
                try
                {
                    var cells = rows[i].Cells.ToList();

                    ExcelRowDto excelModelDto = new ExcelRowDto
                    {
                        col1 = cells[0]?.Value?.ToString() ?? "",
                        col2 = cells[1]?.Value?.ToString() ?? "",
                        col3 = cells[2]?.Value?.ToString() ?? "",
                        col4 = cells[3]?.Value?.ToString() ?? "",
                        col5 = cells[4]?.Value?.ToString() ?? "",
                        col6 = cells[5]?.Value?.ToString() ?? "",
                        col7 = cells[6]?.Value?.ToString() ?? "",
                        col8 = cells[7]?.Value?.ToString() ?? "",
                        col9 = cells[8]?.Value?.ToString() ?? "",
                        col10 = cells[9]?.Value?.ToString() ?? "",
                        col11 = cells[10]?.Value?.ToString() ?? "",
                        col12 = cells[11]?.Value?.ToString() ?? "",
                        col13 = cells[12]?.Value?.ToString() ?? "",
                        col14 = cells[13]?.Value?.ToString() ?? "",
                        col15 = cells[14]?.Value?.ToString() ?? "",
                        col16 = cells[15]?.Value?.ToString() ?? "",
                        col17 = cells[16]?.Value?.ToString() ?? "",
                        col18 = cells[17]?.Value?.ToString() ?? "",
                        col19 = cells[18]?.Value?.ToString() ?? "",
                        col20 = cells[19]?.Value?.ToString() ?? "",
                    };

                    listExcelModelDto.Add(excelModelDto);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Excel Order reports. File: {fileName} at line {i}");
                }
            }

            return listExcelModelDto;
        }

    }
}
