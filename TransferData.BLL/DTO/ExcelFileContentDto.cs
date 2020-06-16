using System;
using System.Collections.Generic;
using System.Text;
using TransferData.BLL.Models;

namespace TransferData.BLL.DTO
{
    public class ExcelFileContentDto
    {
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Список отчётов для сверки заказа.
        /// </summary>
        public IEnumerable<ExcelRowDto> ExcelListRowDto { get; set; }
    }
}
