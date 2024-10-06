using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Abstraction
{
    public interface IPrivilegeService
    {
        Task<BaseResponse> GetPrivilege(PrivilegeSearch search);
        Task<BaseResponse> CreatePrivilege(PrivilegeModel privilege);
        Task<BaseResponse> UpdatePrivilege(PrivilegeModel privilege);
        Task<BaseResponse> DeletePrivilege(int PrivilegeId);
    }
}
