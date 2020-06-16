using Microsoft.Extensions.Logging;
using Q101.ExcelLoader.Concrete.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TransferData.BLL.DTO;
using TransferData.BLL.Models;

namespace TransferData.BLL.Services
{
    public class ExcelConverterService
    {
        /// Логирование.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Конвертер содержимого sftp файла excel report.
        /// </summary>
        public ExcelConverterService(ILogger<ExcelConverterService> logService)
        {
            _logger = logService;
        }

        /// <inheritdoc />
        public ExcelFileContentDto Convert(string filePath, ExcelFileModel excelModel)
        {
            var fileContent = new ExcelFileContentDto
            {
                FileName = Path.GetFileName(filePath),
                ExcelListRowDto = GetExcelModelDto(filePath, excelModel)
            };

            return fileContent;
        }

        /// <summary>
        /// Получить список отчётов по заказам.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="excelModel">Модель файла.</param>
        private IEnumerable<ExcelRowDto> GetExcelModelDto(string filePath, ExcelFileModel excelModel)
        {
            var listExcelModelDto = new List<ExcelRowDto>();

            var rows = excelModel?.Sheets?.FirstOrDefault()?.Rows.ToList();

            if (rows == null || rows.Count == 1)
            {
                return listExcelModelDto;
            }

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
                catch (Exception exc)
                {
                    _logger.LogError(exc, $"Excel Order reports. File: {filePath} at line {i}");
                }
            }

            return listExcelModelDto;
        }
    }
}
