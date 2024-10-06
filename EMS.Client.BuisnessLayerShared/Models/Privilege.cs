using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Models
{
    public class PrivilagesDtoCookie
    {
        public string privilegeName { get; set; }
        public string privilegeGroupName { get; set; }
        public bool canAdd { get; set; }
        public bool canEdit { get; set; }
        public bool canGet { get; set; }
        public bool canDelete { get; set; }
        public bool canExport { get; set; }
    }
    public class ClaimUserData()
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string rolename { get; set; }
        public int roleid { get; set; }
        public string brokerName { get; set; }
        public long? brokerId { get; set; }
        public string logo { get; set; }
    }
    public class PermissionGroupData
    {
        public string privilegeName { get; set; }
        public string privilegeGroupName { get; set; }
        public string path { get; set; }
        public long? serialNumber { get; set; }
        public bool isShowInMenu { get; set; }
    }
}
