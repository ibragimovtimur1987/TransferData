using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransferData.DAL.EF;
using TransferData.DAL.Models;
using TransferData.DAL.Repositories.Interfaces;

namespace TransferData.DAL.Repositories
{
    public class Excel2Repository : GenericRepository<ExcelModel2>, IExcel2Repository
    {
        public Excel2Repository(TransferDataContext context) : base(context)
        {

        }

        public async Task SaveAsync(IEnumerable<ExcelModel2> listExcelModel)
        {
           foreach(ExcelModel2 excelModel in listExcelModel)
           {
               await AddAsyn(excelModel);
           }
        }
    }
}
