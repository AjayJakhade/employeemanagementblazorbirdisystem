using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Models
{
    public class PrivilegeModel:BaseEntity
    {
        public int privilegeid { get; set; }
        public string privilegename { get; set; }
    }
    public class PrivilegeSearch
    {
        public string? PrivilegeName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
