using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Abstraction
{
    public interface IRolePrivilege
    {
        Task<BaseResponse> GetRolePrivilege( );
        Task<BaseResponse> CreateRolePrivilege(RolePrivilegeModel rolePrivilege);
        Task<BaseResponse> UpdateRolePrivilege(RolePrivilegeModel rolePrivilege);
        Task<BaseResponse> DeleteRolePrivilege(long RolePrivilegeId);
    }
}
