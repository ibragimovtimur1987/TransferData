using System;
using System.Collections.Generic;
using System.Text;
using TransferData.DAL.EF;
using TransferData.DAL.Models;
using TransferData.DAL.Repositories.Interfaces;

namespace TransferData.DAL.Repositories
{
    public class ExcelRepository : GenericRepository<ExcelModel>, IExcelRepository
    {
        public ExcelRepository(TransferDataContext context) : base(context)
        {
        }
    }
}
