using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Models
{
    public class DepartmentModel:BaseEntity
    {
        public int deptId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
