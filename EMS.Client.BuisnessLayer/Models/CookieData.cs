using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Models
{
    public class CookieData
    {
        public long userId { get; set; }
        public string userName { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string token { get; set; }
        public List<PrivilagesDTO> privilages { get; set; }

    }
    public class PrivilagesDTO
    {
        public string privilegeName { get; set; }
        public bool canAdd { get; set; }
        public bool canEdit { get; set; }
        public bool canGet { get; set; }
        public bool canDelete { get; set; }
        public bool canExport { get; set; }
    }
}
