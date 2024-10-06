using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Abstractions
{
    public interface ICommonService
    {
        public Task<BaseResponse> CookiesService(string name);
        public Task<BaseResponse> CookieClaimData();
       
    }
}
