using System;
using System.Collections.Generic;
using System.Text;
using TransferData.BLL.Models;

namespace TransferData.BLL.DTO
{
    public class ExcelSheetDto
    {
        /// <summary>
        /// Id Sheet.
        /// </summary>
        public int SheetId { get; set; }

        /// <summary>
        /// Список строк excel файла
        /// </summary>
        public List<ExcelRowDto> ExcelListRowDto { get; set; }
    }
}
