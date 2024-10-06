using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Abstraction
{
    public interface ILoginService
    {
        Task<BaseResponse> ValidateLogin(AccountLoginModel loginModel);
    }
}
