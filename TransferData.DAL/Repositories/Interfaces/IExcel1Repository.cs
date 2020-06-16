using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.DAL.Models;

namespace TransferData.DAL.Repositories.Interfaces
{
    public interface IExcel1Repository : IGenericRepository<ExcelModel1>
    {
       Task SaveAsync(IEnumerable<ExcelModel1> listExcelModel);
    }
}
