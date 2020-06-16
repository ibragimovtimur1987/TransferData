using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransferData.BLL.DTO;
using TransferData.BLL.Infrastructure;
using TransferData.BLL.Services.Interface;
using TransferData.DAL.Models;

namespace TransferData.BLL.Services
{
    public class ExcelProvider: IExcelProvider
    {
        public IEnumerable<ExcelCommonModel> GetTypeExcelTable(ExcelSheetDto excelSheetDto, IAutoMapper _autoMapper )
        {
            if(excelSheetDto.Id==1)
            {
                return excelSheetDto.ExcelListRowDto.Select(_autoMapper.Map<ExcelModel1>);
            }
            else if (excelSheetDto.Id == 2)
            {
                return  excelSheetDto.ExcelListRowDto.Select(_autoMapper.Map<ExcelModel2>);
            }
            return null;
        }
    }
}
