using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Users
{
    public class UserService : IUserService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public UserService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreateUser(BuisnessLayer.Models.UsersModel users)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                users.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder UserList = new StringBuilder();

                UserList.Append("{");

                UserList.Append("\"userid\":\"" + (users.userid) + "\",");
                UserList.Append("\"username\":\"" + (users.username) + "\",");
                UserList.Append("\"email\":\"" + (users.email) + "\",");
                UserList.Append("\"password\":\"" + (users.password) + "\",");
                UserList.Append("\"mobile\":\"" + (users.mobile) + "\",");
                UserList.Append("\"roleid\":\"" + (users.roleid) + "\",");

                UserList.Append("\"entryBy\":\"" + (users.entryBy) + "\",");
                UserList.Append("\"isActive\":" + (users.isActive.ToString().ToLower()) + "");
                UserList.Append("}");
                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreateUsers, UserList.ToString());

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> DeleteUser(long UserId)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                _callServices.SetSeriveURL(_callServices);
               
                string b = "UserId=" + UserId;

                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeleteUsers, b);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> GetUser(UserSearch userSearch)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                _callServices.SetSeriveURL(_callServices);
                StringBuilder userlist = new StringBuilder();
                string b = "UserName=" + userSearch.UserName + "&Email=" + userSearch.Email;

                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetUsers, b);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateUser(BuisnessLayer.Models.UsersModel users)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                users.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder UserList = new StringBuilder();

                UserList.Append("{");

                UserList.Append("\"userid\":\"" + (users.userid) + "\",");
                UserList.Append("\"username\":\"" + (users.username) + "\",");
                UserList.Append("\"email\":\"" + (users.email) + "\",");
                UserList.Append("\"password\":\"" + (users.password) + "\",");
                UserList.Append("\"mobile\":\"" + (users.mobile) + "\",");
                UserList.Append("\"roleid\":\"" + (users.roleid) + "\",");
              
                UserList.Append("\"entryBy\":\"" + (users.entryBy) + "\",");
                UserList.Append("\"isActive\":" + (users.isActive.ToString().ToLower()) + "");
                UserList.Append("}");
                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdateUsers, UserList.ToString());

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
