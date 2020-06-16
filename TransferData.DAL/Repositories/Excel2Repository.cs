using System;
using System.Collections.Generic;
using System.Text;
using TransferData.DAL.Models;

namespace TransferData.DAL.Repositories
{
    public class Excel2Repository: GenericRepository<Models.ExcelModel2>
    {
        public Excel2Repository(EF.TransferDataContext context) : base(context)
        {
        }
    }
}
