using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public DepartmentService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreateDepartment(DepartmentModel departmentModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                departmentModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder departmentlist = new StringBuilder();

                departmentlist.Append("{");

                departmentlist.Append("\"deptId\":\"" + (departmentModel.deptId) + "\",");
                departmentlist.Append("\"name\":\"" + (departmentModel.name) + "\",");
                departmentlist.Append("\"description\":\"" + (departmentModel.description) + "\",");

                departmentlist.Append("\"entryBy\":\"" + (departmentModel.entryBy) + "\",");
                departmentlist.Append("\"isActive\":" + (departmentModel.isActive.ToString().ToLower()) + "");
                departmentlist.Append("}");
                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreateDepartment, departmentlist.ToString());

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> DeleteDepartment(int departmentid)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                _callServices.SetSeriveURL(_callServices);

                string b = "departmentid=" + departmentid;

                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeleteDepartment, b);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> GetDepartment()
        {
            BaseResponse response = new BaseResponse();
            try
            {

                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetDepartment, "");
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateDepartment(DepartmentModel departmentModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                departmentModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder departmentlist = new StringBuilder();

                departmentlist.Append("{");

                departmentlist.Append("\"deptId\":\"" + (departmentModel.deptId) + "\",");
                departmentlist.Append("\"name\":\"" + (departmentModel.name) + "\",");
                departmentlist.Append("\"description\":\"" + (departmentModel.description) + "\",");

                departmentlist.Append("\"entryBy\":\"" + (departmentModel.entryBy) + "\",");
                departmentlist.Append("\"isActive\":" + (departmentModel.isActive.ToString().ToLower()) + "");
                departmentlist.Append("}");
                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdateDepartment, departmentlist.ToString());

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
