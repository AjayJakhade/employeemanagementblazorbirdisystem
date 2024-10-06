using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Common
{
    public class CommonService : ICommonService
    {
        private IHttpContextAccessor _context;
        private long userid;
        public string rolename;
        public CommonService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public async Task<BaseResponse> CookieClaimData()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = _context.HttpContext.User;
                var claimData = new ClaimUserData();
                if (user is not null && user.Claims.Count() > 0)
                {

                    claimData.username = user.Identity.Name.ToString();
                    claimData.rolename = (user.FindFirst(ClaimTypes.Role).Value);
                    claimData.userid = Convert.ToInt64(user.FindFirst(ClaimTypes.Sid).Value);
                    claimData.roleid = Convert.ToInt32(user.FindFirst("RoleId").Value);

                    response.result = claimData;
                    response.isSuccess = true;

                }
                else
                {
                    response.isSuccess = false;
                    response.message = "no claim.";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> CookiesService(string name)
        {
            BaseResponse response = new BaseResponse() { isSuccess = false, message = "shared name is required." };
            PrivilagesDtoCookie privilagesDto = new PrivilagesDtoCookie();
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var user = _context.HttpContext.User;
                    var data = user.FindFirst("privilege").Value;
                    List<PrivilagesDtoCookie> deserializedPrivileges = JsonSerializer.Deserialize<List<PrivilagesDtoCookie>>(data);
                    var temp = deserializedPrivileges.Where(c => c.privilegeName == name).FirstOrDefault();
                    if (temp is not null)
                    {
                        privilagesDto.canAdd = temp.canAdd;
                        privilagesDto.canEdit = temp.canEdit;
                        privilagesDto.canGet = temp.canGet;
                        privilagesDto.canDelete = temp.canDelete;
                        privilagesDto.canExport = temp.canExport;
                        response.result = privilagesDto;
                        response.isSuccess = true;
                        response.message = string.Empty;
                    }
                    else
                    {
                        response.result = false;
                        response.message = "No Privilage";
                    }

                }
                else
                {
                    response.result = false;
                    response.message = "Name is required";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> PrivilegeGroupsService()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = _context.HttpContext.User;
                if (user != null)
                {
                    var data = user.FindFirst("Privilege").Value;
                    List<PermissionGroupData> deserializedPrivileges = JsonSerializer.Deserialize<List<PermissionGroupData>>(data);
                    response.isSuccess = true;
                    response.result = deserializedPrivileges;
                }
                else
                {
                    response.message = "no group";
                    response.isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                response.result = false;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
