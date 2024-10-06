using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayer.Abstraction
{
    public interface IUserService
    {
        Task<BaseResponse> GetUser(UserSearch userSearch);
        Task<BaseResponse> CreateUser(UsersModel users);
        Task<BaseResponse> UpdateUser(UsersModel users);
        Task<BaseResponse> DeleteUser(long UserId);
    }
}
