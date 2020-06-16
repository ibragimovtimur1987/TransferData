using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.DAL.EF;
using TransferData.DAL.Models;
using TransferData.DAL.Repositories.Interfaces;

namespace TransferData.DAL.Repositories
{
    public class Excel1Repository : GenericRepository<ExcelModel1>, IExcel1Repository
    {
        public Excel1Repository(TransferDataContext context) : base(context)
        {

        }

        public async Task SaveAsync(IEnumerable<ExcelModel1> listExcelModel)
        {
           foreach(ExcelModel1 excelModel in listExcelModel)
           {
               await AddAsyn(excelModel);
           }
        }
    }
}
