using EMS.Client.BuisnessLayerShared.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMS.Client.Application
{
    public class CallServices
    {
        private IConfiguration _config;
        private string ServiceURL { get; set; }
        public CallServices(IConfiguration config)
        {
            _config = config;
        }
        public CallServices SetSeriveURL(CallServices objCS)
        {
            objCS.ServiceURL =  _config["Services:URL"];
            return objCS;
        }
       /* public enum Source
        {
            CRM_Master,
            db_health
        }*/
        public enum ServiceCallPurpose
        {
           
            LoginUser,
            GetPrivilege,
            CreatePrivilege,
            UpdatePrivilege,
            DeletePrivilege,
            GetUsers,
            CreateUsers,
            UpdateUsers,
            DeleteUsers,
            GetRole,
            GetRolePrivilege,
            CreateRolePrivilege,
            UpdateRolePrivilege,
            DeleteRolePrivilege,
            GetEmployee,
            CreateEmployee,
            UpdateEmployee,
            DeleteEmployee,
            GetDepartment,
            CreateDepartment,
            UpdateDepartment,
            DeleteDepartment,
            GetSalary,
            CreateSalary,
            UpdateSalary,
            DeleteSalary

        }
        private string GetServiceURL(ServiceCallPurpose purpose)
        {
            string SURL = "";

            switch (purpose)
            {
              
                
                case ServiceCallPurpose.LoginUser:
                    SURL += "/api/Login/GetLogin";
                    break;
                case ServiceCallPurpose.GetPrivilege:
                    SURL += "/api/Privilege/GetPrivilege";
                    break;
                case ServiceCallPurpose.CreatePrivilege:
                    SURL += "/api/Privilege/CreatePrivilege";
                    break;
                case ServiceCallPurpose.UpdatePrivilege:
                    SURL += "/api/Privilege/UpdatePrivilege";
                    break;
                case ServiceCallPurpose.DeletePrivilege:

                    SURL += "/api/Privilege/DeletePrivilege";
                    break;
                case ServiceCallPurpose.CreateUsers:
                    SURL += "/api/Users/CreateUsers";
                    break;
                case ServiceCallPurpose.GetUsers:
                    SURL += "/api/Users/GetUsers";
                    break;
                case ServiceCallPurpose.UpdateUsers:
                    SURL += "/api/Users/UpdateUsers";
                    break;
                case ServiceCallPurpose.DeleteUsers:
                    SURL += "/api/Users/DeleteUsers";
                    break;
                case ServiceCallPurpose.GetRole:
                    SURL += "/api/Role/GetRole";
                    break;
                case ServiceCallPurpose.GetRolePrivilege:
                    SURL += "/api/RolePrivilege/GetRolePrivilege";
                    break;
                case ServiceCallPurpose.CreateRolePrivilege:
                    SURL += "/api/RolePrivilege/CreateRolePrivilege";
                    break;
                case ServiceCallPurpose.UpdateRolePrivilege:
                    SURL += "/api/RolePrivilege/UpdateRolePrivilege";
                    break;
                case ServiceCallPurpose.DeleteRolePrivilege:
                    SURL += "/api/RolePrivilege/DeleteRolePrivilege";
                    break;
                case ServiceCallPurpose.GetEmployee:
                    SURL += "/api/Employee/GetEmployee";
                    break;
                case ServiceCallPurpose.CreateEmployee:
                    SURL += "/api/Employee/CreateEmployee";
                    break;
                case ServiceCallPurpose.UpdateEmployee:
                    SURL += "/api/Employee/UpdateEmployee";
                    break;
                case ServiceCallPurpose.DeleteEmployee:
                    SURL += "/api/Employee/DeleteEmployee";
                    break;
                case ServiceCallPurpose.GetDepartment:
                    SURL += "/api/Department/GetDepartment";
                    break;
                case ServiceCallPurpose.CreateDepartment:
                    SURL += "/api/Department/CreateDepartment";
                    break;
                case ServiceCallPurpose.UpdateDepartment:
                    SURL += "/api/Department/UpdateDepartment";
                    break;
                case ServiceCallPurpose.DeleteDepartment:
                    SURL += "/api/Department/DeleteDepartment";
                    break;
                case ServiceCallPurpose.GetSalary:
                    SURL += "/api/Salary/GetSalary";
                    break;
                case ServiceCallPurpose.CreateSalary:
                    SURL += "/api/Salary/CreateSalary";
                    break;
                case ServiceCallPurpose.UpdateSalary:
                    SURL += "/api/Salary/UpdateSalary";
                    break;
                case ServiceCallPurpose.DeleteSalary:
                    SURL += "/api/Salary/DeleteSalary";
                    break;

            }
            return ServiceURL + SURL;
        }
        public async Task<BaseResponse> GetRequest(ServiceCallPurpose Purpose, string requestJSON)
        {
            BaseResponse response = new();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, GetServiceURL(Purpose) + (string.IsNullOrWhiteSpace(requestJSON) ? "" : ("?" + requestJSON)));
           
            try
            {
                var serviceResponse = await client.SendAsync(request);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    response = JsonSerializer.Deserialize<BaseResponse>(await serviceResponse.Content.ReadAsStringAsync());

                }
                else
                {
                    response.isSuccess = false;
                    response.message = await serviceResponse.Content.ReadAsStringAsync();
                    response.result = null;
                    response.status = "200";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = true;
                response.message = "Exception : " + ex.Message;
                response.result = null;
                response.status = "200";
            }

            return response;
        }
        public async Task<BaseResponse> PostRequest(ServiceCallPurpose Purpose, string requestJSON)
        {

            BaseResponse response = new();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, GetServiceURL(Purpose));
            var content = new StringContent(requestJSON, Encoding.UTF8, "application/json");

            request.Content = content;
            try
            {
                var serviceResponse = await client.SendAsync(request);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    response = JsonSerializer.Deserialize<BaseResponse>(await serviceResponse.Content.ReadAsStringAsync());

                }
                else
                {
                    response.isSuccess = false;
                    response.message = await serviceResponse.Content.ReadAsStringAsync();
                    response.result = null;
                    response.status = "200";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = true;
                response.message = "Exception : " + ex.Message;
                response.result = null;
                response.status = "200";
            }

            return response;

        }
        public async Task<BaseResponse> UpdateRequest(ServiceCallPurpose Purpose, string requestJSON)
        {
            BaseResponse response = new();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, GetServiceURL(Purpose));
            var content = new StringContent(requestJSON, Encoding.UTF8, "application/json");

            request.Content = content;
            try
            {
                var serviceResponse = await client.SendAsync(request);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    response = JsonSerializer.Deserialize<BaseResponse>(await serviceResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    response.isSuccess = false;
                    response.message = await serviceResponse.Content.ReadAsStringAsync();
                    response.result = null;
                    response.status = "200";
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = "Exception: " + ex.Message;
                response.result = null;
                response.status = "500"; 
            }

            return response;
        }
        public async Task<BaseResponse> DeleteRequest(ServiceCallPurpose Purpose, string requestJSON)
        {
            BaseResponse response = new();
            var client = new HttpClient();
            var requestUrl = GetServiceURL(Purpose) + (string.IsNullOrWhiteSpace(requestJSON) ? "" : ("?" + requestJSON));
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUrl);

            try
            {
                var serviceResponse = await client.SendAsync(request);
                if (serviceResponse.IsSuccessStatusCode)
                {
                    response = JsonSerializer.Deserialize<BaseResponse>(await serviceResponse.Content.ReadAsStringAsync());
                }
                else
                {
                    response.isSuccess = false;
                    response.message = await serviceResponse.Content.ReadAsStringAsync();
                    response.result = null;
                    response.status = "200"; 
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = "Exception: " + ex.Message;
                response.result = null;
                response.status = "500";
            }

            return response;
        }


    }
}
