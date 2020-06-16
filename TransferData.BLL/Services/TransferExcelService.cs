﻿using Microsoft.AspNetCore.Http;
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

namespace TransferData.BLL.Services
{
    public class TransferExcelService: ITransferExcelService
    {
        /// Логирование.
        /// </summary>
        private readonly ILogger _logger;
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
        private readonly IAutoMapper _autoMapper;
        public TransferExcelService(ILogger<TransferExcelService> logService, IExcelFileLoader excelLoader, IGenericRepository<ExcelModel1> excelRepository, IGenericRepository<ExcelModel2> excel2Repository)
        {
            _logger = logService;
            _excelLoader = excelLoader;
            _excel1Repository = excelRepository;
            _excel2Repository = excel2Repository;
        }
        public async Task Save(IFormFile excelModelForm)
        {
            using (var fs = excelModelForm.OpenReadStream())
            {
               await Save(fs, excelModelForm.FileName);
            }
        }
        public async Task Save(Stream fs, string fileName)
        {
            List<ExcelSheetDto> listExcelSheetDto = Convert(fs, fileName);

            foreach (var excelSheetDto in listExcelSheetDto)
            {
                if (excelSheetDto.Id == 1)
                {
                    IEnumerable<ExcelModel1> exelModels = excelSheetDto.ExcelListRowDto.Select(_autoMapper.Map<ExcelModel1>);
                    await _excel1Repository.SaveAsync(exelModels);
                }
                else if (excelSheetDto.Id == 2)
                {
                    IEnumerable<ExcelModel2> exelModels = excelSheetDto.ExcelListRowDto.Select(_autoMapper.Map<ExcelModel2>);
                    await _excel2Repository.SaveAsync(exelModels);
                }
            }
        }
            /// <inheritdoc />
            private List<ExcelSheetDto> Convert(Stream fs, string fileName)
        {
          
                var excelFile = _excelLoader.Load(fs);
                int i = 0;
                var listExcelSheetDto = new List<ExcelSheetDto>();
                foreach (var sheet in excelFile?.Sheets)
                {
                    var excelFileContentDto = new ExcelSheetDto
                    {
                        Id = i,
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
        private IEnumerable<ExcelRowDto> GetExcelModelDto(ExcelSheetModel excelSheetModel,string fileName)
        {
            var listExcelModelDto = new List<ExcelRowDto>();

           // var rows = excelModel?.Sheets?.FirstOrDefault()?.Rows.ToList();

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
                    _logger.LogError(ex, $"Excel Order reports. File: {fileName} at line {i}");
                }
            }

            return listExcelModelDto;
        }
    }
}