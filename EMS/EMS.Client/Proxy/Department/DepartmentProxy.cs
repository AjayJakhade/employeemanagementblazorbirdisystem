using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace EMS.Client.Proxy.Department
{
    public class DepartmentProxy : IDepartmentService
    {
        private readonly HttpClient httpClient;
        public DepartmentProxy(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<BaseResponse> CreateDepartment(DepartmentModel departmentModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/CreateDepartment", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> DeleteDepartment(int departmentid)
        {
            var data = await httpClient.PostAsJsonAsync("/api/DeleteDepartment", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> GetDepartment()
        {
            var data = await httpClient.PostAsJsonAsync("/api/GetDepartment", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> UpdateDepartment(DepartmentModel departmentModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/UpdateDepartment", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }
    }
}
