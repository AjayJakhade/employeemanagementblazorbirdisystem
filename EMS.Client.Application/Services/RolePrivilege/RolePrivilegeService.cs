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

namespace EMS.Client.Application.Services.RolePrivilege
{
    public class RolePrivilegeService : IRolePrivilege
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public RolePrivilegeService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreateRolePrivilege(BuisnessLayer.Models.RolePrivilegeModel rolePrivilege)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                StringBuilder roleprivilegeData = new StringBuilder();
                var users = await _common.CookieClaimData();
                var userdata = (ClaimUserData)users.result;
                var userid = userdata.userid;
                rolePrivilege.entryBy = userid;
                roleprivilegeData.Append("{");

                roleprivilegeData.Append("\"roleprivilegeid\":\"" + (rolePrivilege.roleprivilegeid) + "\",");
                roleprivilegeData.Append("\"roleid\":\"" + (rolePrivilege.roleid) + "\",");
                roleprivilegeData.Append("\"privilegeid\":\"" + (rolePrivilege.privilegeid) + "\",");
               
                roleprivilegeData.Append("\"canadd\":" + (rolePrivilege.canadd.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canedit\":" + (rolePrivilege.canedit.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"candelete\":" + (rolePrivilege.candelete.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canexport\":" + (rolePrivilege.canexport.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canget\":" + (rolePrivilege.canget.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"entryBy\":\"" + (rolePrivilege.entryBy) + "\",");
                roleprivilegeData.Append("\"isActive\":" + (rolePrivilege.isActive.ToString().ToLower()) + "");
                roleprivilegeData.Append("}");

                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreateRolePrivilege, roleprivilegeData.ToString());
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async  Task<BaseResponse> DeleteRolePrivilege(long RolePrivilegeId)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                string roleprivilegerequest = "RolePrivilegeId=" + RolePrivilegeId;
                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeleteRolePrivilege,roleprivilegerequest);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> GetRolePrivilege()
        {
            BaseResponse response = new BaseResponse();
            try
            {
               
                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetRolePrivilege,"");
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateRolePrivilege(BuisnessLayer.Models.RolePrivilegeModel rolePrivilege)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                StringBuilder roleprivilegeData = new StringBuilder();
                var users = await _common.CookieClaimData();
                var userdata = (ClaimUserData)users.result;
                var userid = userdata.userid;
                rolePrivilege.entryBy = userid;
                roleprivilegeData.Append("{");

                roleprivilegeData.Append("\"roleprivilegeid\":\"" + (rolePrivilege.roleprivilegeid) + "\",");
                roleprivilegeData.Append("\"roleid\":\"" + (rolePrivilege.roleid) + "\",");
                roleprivilegeData.Append("\"privilegeid\":\"" + (rolePrivilege.privilegeid) + "\",");

                roleprivilegeData.Append("\"canadd\":" + (rolePrivilege.canadd.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canedit\":" + (rolePrivilege.canedit.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"candelete\":" + (rolePrivilege.candelete.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canexport\":" + (rolePrivilege.canexport.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"canget\":" + (rolePrivilege.canget.ToString().ToLower()) + ",");
                roleprivilegeData.Append("\"entryBy\":\"" + (rolePrivilege.entryBy) + "\",");
                roleprivilegeData.Append("\"isActive\":" + (rolePrivilege.isActive.ToString().ToLower()) + "");
                roleprivilegeData.Append("}");

                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdateRolePrivilege, roleprivilegeData.ToString());
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
