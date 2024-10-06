using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Models
{
    public class RolePrivilegeModel:BaseEntity
    {
        public long roleprivilegeid { get; set; }
        public int roleid { get; set; }
        public string rolename {  get; set; }
        public int privilegeid { get; set; }
        public string privilegename { get; set; }

        public bool canadd { get; set; } = false;
        public bool canedit { get; set; } = false;
        public bool candelete { get; set; } = false;
        public bool canexport { get; set; } = false;
        public bool canget { get; set; } = false;
    }
}
