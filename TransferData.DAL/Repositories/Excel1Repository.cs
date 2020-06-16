using System;
using System.Collections.Generic;
using System.Text;
using TransferData.DAL.EF;

namespace TransferData.DAL.Repositories
{
    public class Excel1Repository : GenericRepository<Models.ExcelModel1>
    {
        public Excel1Repository(TransferDataContext context) : base(context)
        {
        }
    }
}
