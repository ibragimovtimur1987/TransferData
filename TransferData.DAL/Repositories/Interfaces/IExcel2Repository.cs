using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.DAL.Models;

namespace TransferData.DAL.Repositories.Interfaces
{
    public interface IExcel2Repository : IGenericRepository<ExcelModel2>
    {
       Task SaveAsync(IEnumerable<ExcelModel2> listExcelModel);
    }
}
