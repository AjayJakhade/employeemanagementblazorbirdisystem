using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public EmployeeService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreateEmployee(EmployeeModel employeeModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                employeeModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder employeelist = new StringBuilder();

                employeelist.Append("{");

                employeelist.Append("\"empId\":\"" + (employeeModel.empId) + "\",");
                employeelist.Append("\"firstName\":\"" + (employeeModel.firstName) + "\",");
                employeelist.Append("\"lastName\":\"" + (employeeModel.lastName) + "\",");
                employeelist.Append("\"phone\":\"" + (employeeModel.phone) + "\",");
                employeelist.Append("\"email\":\"" + (employeeModel.email) + "\",");
                employeelist.Append("\"position\":\"" + (employeeModel.position) + "\",");
                employeelist.Append("\"deptId\":\"" + (employeeModel.deptId) + "\",");
                employeelist.Append("\"entryBy\":\"" + (employeeModel.entryBy) + "\",");
                employeelist.Append("\"isActive\":" + (employeeModel.isActive.ToString().ToLower()) + "");
                employeelist.Append("}");
                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreateEmployee, employeelist.ToString());

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> DeleteEmployee(int employeeid)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                _callServices.SetSeriveURL(_callServices);

                string b = "employeeid=" + employeeid;

                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeleteEmployee, b);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> GetEmployee()
        {
            BaseResponse response = new BaseResponse();
            try
            {

                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetEmployee, "");
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateEmployee(EmployeeModel employeeModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                employeeModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder employeelist = new StringBuilder();

                employeelist.Append("{");

                employeelist.Append("\"empId\":\"" + (employeeModel.empId) + "\",");
                employeelist.Append("\"firstName\":\"" + (employeeModel.firstName) + "\",");
                employeelist.Append("\"lastName\":\"" + (employeeModel.lastName) + "\",");
                employeelist.Append("\"phone\":\"" + (employeeModel.phone) + "\",");
                employeelist.Append("\"email\":\"" + (employeeModel.email) + "\",");
                employeelist.Append("\"position\":\"" + (employeeModel.position) + "\",");
                employeelist.Append("\"deptId\":\"" + (employeeModel.deptId) + "\",");
                employeelist.Append("\"entryBy\":\"" + (employeeModel.entryBy) + "\",");
                employeelist.Append("\"isActive\":" + (employeeModel.isActive.ToString().ToLower()) + "");
                employeelist.Append("}");
                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdateEmployee, employeelist.ToString());

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
