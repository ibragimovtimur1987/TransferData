using System;
using System.Collections.Generic;
using System.Text;

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
        public ExcelReportFileConverter(ILogger<IdlSftpExcelReportFileConverter> logService)
        {
            _logger = logService;
        }

        /// <inheritdoc />
        public ExcelReportFileConverter Convert(string filePath, ExcelFileModel excelModel)
        {
            var fileContent = new IdlSftpExcelReportFileContentDto
            {
                FileName = Path.GetFileName(filePath),
                OrderReports = GetOrderReports(filePath, excelModel)
            };

            return fileContent;
        }

        /// <summary>
        /// Получить список отчётов по заказам.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="excelModel">Модель файла.</param>
        private IEnumerable<IdlSftpExcelReportFileOrderReportDto> GetOrderReports(string filePath, ExcelFileModel excelModel)
        {
            var reports = new List<IdlSftpExcelReportFileOrderReportDto>();

            var rows = excelModel?.Sheets?.FirstOrDefault()?.Rows.ToList();

            if (rows == null || rows.Count == 1)
            {
                return reports;
            }

            for (int i = 1; i < rows.Count; i++)
            {
                try
                {
                    var cells = rows[i].Cells.ToList();

                    var report = new IdlSftpExcelReportFileOrderReportDto
                    {
                        OrderId = cells[1]?.GetInt() ?? 0,
                        ImportDate = cells[2]?.GetDateTime(),
                        PrintLabelDate = cells[3]?.GetDateTime(),
                        PackagingStartDate = cells[4]?.GetDateTime(),
                        CancelDate = cells[5]?.GetDateTime(),
                        PackageDate = cells[6]?.GetDateTime(),
                        ReadyToDispatchDate = cells[7]?.GetDateTime(),
                        DispatchDate = cells[8]?.GetDateTime()
                    };

                    if (report.OrderId == 0)
                    {
                        continue;
                    }

                    reports.Add(report);
                }
                catch (Exception exc)
                {
                    _logger.LogError(exc, $"IDL SFTP Excel Order reports. File: {filePath} at line {i}");
                }
            }

            return reports;
        }
    }
}
