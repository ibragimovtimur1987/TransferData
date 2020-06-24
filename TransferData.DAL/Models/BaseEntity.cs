using System;
using System.Collections.Generic;
using System.Text;

namespace TransferData.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
