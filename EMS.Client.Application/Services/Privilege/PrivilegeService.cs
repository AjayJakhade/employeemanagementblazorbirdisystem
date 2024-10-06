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

namespace EMS.Client.Application.Services.Privilege
{
    public class PrivilegeService : IPrivilegeService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public PrivilegeService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreatePrivilege(BuisnessLayer.Models.PrivilegeModel privilege)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var users = await _common.CookieClaimData();
                var userdata = (ClaimUserData)users.result;
                var userid = userdata.userid;
                privilege.entryBy = userid;

                _callServices.SetSeriveURL(_callServices);
                StringBuilder privilegelist = new StringBuilder();
                privilegelist.Append("{");
                privilegelist.Append("\"privilegename\":\"" + privilege.privilegename + "\",");

                privilegelist.Append("\"isActive\":" + privilege.isActive.ToString().ToLower() + ",");
                if (string.IsNullOrEmpty(privilege.entryBy.ToString()))
                {
                    privilegelist.Append("\"entryBy\":null,");
                }
                else
                {
                    privilegelist.Append("\"entryBy\":" + privilege.entryBy + ",");
                }
                privilegelist.Append("\"privilegeid\":" + privilege.privilegeid + "");
                privilegelist.Append("}");

                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreatePrivilege, privilegelist.ToString());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;

            }
            return response;
        }

        public async Task<BaseResponse> DeletePrivilege(int PrivilegeId)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                string privilagelist = "PrivilegeId=" + PrivilegeId;
                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeletePrivilege, privilagelist);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> GetPrivilege(PrivilegeSearch search)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                string privilagelist = "PrivilegeName=" + search.PrivilegeName  + "&IsActive=" + search.IsActive;
                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetPrivilege, privilagelist);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> UpdatePrivilege(BuisnessLayer.Models.PrivilegeModel privilege)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var users = await _common.CookieClaimData();
                var userdata = (ClaimUserData)users.result;
                var userid = userdata.userid;
                privilege.entryBy = userid;

                _callServices.SetSeriveURL(_callServices);
                StringBuilder privilegelist = new StringBuilder();
                privilegelist.Append("{");
                privilegelist.Append("\"privilegename\":\"" + privilege.privilegename + "\",");
              
                privilegelist.Append("\"isActive\":" + privilege.isActive.ToString().ToLower() + ",");
                if (string.IsNullOrEmpty(privilege.entryBy.ToString()))
                {
                    privilegelist.Append("\"entryBy\":null,");
                }
                else
                {
                    privilegelist.Append("\"entryBy\":" + privilege.entryBy + ",");
                }
                privilegelist.Append("\"privilegeid\":" + privilege.privilegeid + "");
                privilegelist.Append("}");

                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdatePrivilege, privilegelist.ToString());
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
