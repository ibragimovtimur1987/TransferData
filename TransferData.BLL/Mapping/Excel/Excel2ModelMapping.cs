using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TransferData.BLL.Models;
using TransferData.DAL.Models;

namespace TransferData.BLL.Mapping.Excel
{
    public class Excel2ModelMapping : Profile
    {
        public Excel2ModelMapping()
        {
            CreateMap<ExcelRowDto, ExcelModel2>();
        }
    }
}
