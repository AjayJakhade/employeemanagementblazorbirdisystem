using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Common
{
    public class BaseEntity
    {
        public DateTime createdOn { get; set; } = DateTime.Now;
        public long? entryBy { get; set; }
        public bool isActive { get; set; } = true;
    }
}
