using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EMS.Client.Proxy.Salary
{
    public class SalaryProxy : ISalaryService
    {
        private readonly HttpClient httpClient;
        public SalaryProxy(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<BaseResponse> CreateSalary(SalaryModel salaryModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/CreateSalary", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> DeleteSalary(int salaryid)
        {
            var data = await httpClient.PostAsJsonAsync("/api/DeleteSalary", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> GetSalary()
        {
            var data = await httpClient.PostAsJsonAsync("/api/GetSalary", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }

        public async Task<BaseResponse> UpdateSalary(SalaryModel salaryModel)
        {
            var data = await httpClient.PostAsJsonAsync("/api/UpdateSalary", "");
            var content = await data.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<BaseResponse>(content);
            return resp;
        }
    }
}
