using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application.Services.Salary
{
    public class SalaryService : ISalaryService
    {
        private CallServices _callServices;
        private readonly ICommonService _common;
        public SalaryService(CallServices callServices, ICommonService common)
        {
            _callServices = callServices;
            _common = common;
            _callServices = _callServices.SetSeriveURL(_callServices);

        }
        public async Task<BaseResponse> CreateSalary(SalaryModel salaryModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                salaryModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder salarylist = new StringBuilder();
                salarylist.Append("{");
                salarylist.Append("\"salaryId\":\"" + (salaryModel.salaryId) + "\",");
                salarylist.Append("\"empId\":\"" + (salaryModel.empId) + "\",");
                salarylist.Append("\"amount\":\"" + (salaryModel.amount) + "\",");
                salarylist.Append("\"entryBy\":\"" + (salaryModel.entryBy) + "\",");
                salarylist.Append("\"isActive\":" + (salaryModel.isActive.ToString().ToLower()) + "");
                salarylist.Append("}");
                response = await _callServices.PostRequest(CallServices.ServiceCallPurpose.CreateSalary, salarylist.ToString());

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async  Task<BaseResponse> DeleteSalary(int salaryid)
        {
            BaseResponse response = new BaseResponse();
            try
            {

                _callServices.SetSeriveURL(_callServices);

                string b = "salaryid=" + salaryid;

                response = await _callServices.DeleteRequest(CallServices.ServiceCallPurpose.DeleteSalary, b);
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> GetSalary()
        {
            BaseResponse response = new BaseResponse();
            try
            {

                response = await _callServices.GetRequest(CallServices.ServiceCallPurpose.GetSalary, "");
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.isSuccess = false;
            }
            return response;
        }

        public async Task<BaseResponse> UpdateSalary(SalaryModel salaryModel)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userdata = await _common.CookieClaimData();
                var user = (ClaimUserData)userdata.result;
                long userid = user.userid;
                salaryModel.entryBy = userid;
                _callServices.SetSeriveURL(_callServices);
                StringBuilder salarylist = new StringBuilder();
                salarylist.Append("{");
                salarylist.Append("\"salaryId\":\"" + (salaryModel.salaryId) + "\",");
                salarylist.Append("\"empId\":\"" + (salaryModel.empId) + "\",");
                salarylist.Append("\"amount\":\"" + (salaryModel.amount) + "\",");
                salarylist.Append("\"entryBy\":\"" + (salaryModel.entryBy) + "\",");
                salarylist.Append("\"isActive\":" + (salaryModel.isActive.ToString().ToLower()) + "");
                salarylist.Append("}");
                response = await _callServices.UpdateRequest(CallServices.ServiceCallPurpose.UpdateSalary, salarylist.ToString());

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
