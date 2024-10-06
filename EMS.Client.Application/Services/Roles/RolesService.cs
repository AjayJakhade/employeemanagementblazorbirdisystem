using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Roles
{
    public class RolesService : IRoleService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public RolesService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> GetRoles()
        {
            BaseResponse response = new BaseResponse();
            try
            {

                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetRole, "");
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }
    }
}
