using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Models
{
    public class EmployeeModel:BaseEntity
    {
        public int empId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string position { get; set; }
        public int deptId { get; set; }
        public string name { get; set; }
    }
}
