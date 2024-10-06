using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public LoginService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> ValidateLogin(AccountLoginModel loginModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                string loginUser = "username=" + loginModel.username + "&password=" + loginModel.password;
                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.LoginUser, loginUser);
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
