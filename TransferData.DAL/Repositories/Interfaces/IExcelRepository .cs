using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.DAL.Models;

namespace TransferData.DAL.Repositories.Interfaces
{
    public interface IExcelRepository : IGenericRepository<ExcelModel>
    {
       Task SaveAsync(IEnumerable<ExcelModel> listExcelModel);
    }
}
