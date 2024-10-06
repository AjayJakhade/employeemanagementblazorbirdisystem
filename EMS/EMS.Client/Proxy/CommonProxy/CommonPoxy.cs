using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using System.Net.Http.Json;
using System.Text.Json;

namespace EMS.Client.Proxy.CommonProxy
{
    public class CommonProxy : ICommonService
    {
        private readonly HttpClient httpClient;
        public CommonProxy(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        public async Task<BaseResponse> CookieClaimData()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var data = await httpClient.PostAsJsonAsync("/api/context/claim", "");
                var content = await data.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<BaseResponse>(content);
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
            Shared shared = new Shared() { name = name };
            BaseResponse response = new BaseResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var data = await httpClient.PostAsJsonAsync("/api/context/getuser", shared);
                    var content = await data.Content.ReadAsStringAsync();
                    response = JsonSerializer.Deserialize<BaseResponse>(content);
                }

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
