using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TransferData.BLL.Models;
using TransferData.DAL.Models;

namespace TransferData.BLL.Mapping.Excel
{
    public class Excel1ModelMapping : Profile
    {
        public Excel1ModelMapping()
        {
            CreateMap<ExcelRowDto, ExcelModel1>();
        }
    }
}
