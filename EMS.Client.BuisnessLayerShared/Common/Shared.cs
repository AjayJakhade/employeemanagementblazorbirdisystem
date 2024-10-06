using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Common
{
    public class Shared
    {
        public string name { get; set; }
        public string nameextension { get; set; }

    }
    public class FormMessage
    {
        public string message { get; set; }
        public string cssClass { get; set; }
    }
    public class BaseResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public string status { get; set; }
        public object result { get; set; }

    }
}
