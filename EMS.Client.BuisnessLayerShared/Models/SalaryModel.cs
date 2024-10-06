using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Models
{
    public class SalaryModel:BaseEntity
    {
        public int salaryId { get; set; }
        public int empId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public decimal amount { get; set; }
    }
}
