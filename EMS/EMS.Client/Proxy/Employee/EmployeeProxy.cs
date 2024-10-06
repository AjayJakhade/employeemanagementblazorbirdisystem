using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EMS.Client.Proxy.Employee
{
    public class EmployeeProxy : IEmployeeService
    {
        private readonly HttpClient httpClient;
        public EmployeeProxy(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<BaseResponse> CreateEmployee(EmployeeModel employeeModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/CreateEmployee", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> DeleteEmployee(int employeeid)
        {
            var data = await httpClient.PostAsJsonAsync("/api/DeleteEmployee", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> GetEmployee()
        {
            var data = await httpClient.PostAsJsonAsync("/api/GetEmployee", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> UpdateEmployee(EmployeeModel employeeModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/UpdateEmployee", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }
    }
}
