using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Text.Json;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Models;

namespace EMS.Components.Login
{
    public partial class LoginComponent
    {
        [SupplyParameterFromForm] private AccountLoginModel LoginModel { get; set; } = new();
        private CookieData CookieData { get; set; } = new();
        private PrivilagesDTO PrivilagesDTO { get; set; } = new();
        [Inject] private ILoginService _loginservice { get; set; }
        [Inject] private NavigationManager objNav { get; set; }
        private FormMessage objFormMessage { get; set; } = new();
        [Inject] private IHttpContextAccessor context { get; set; }
        [Inject] private ICommonService _common { get; set; }
        public bool isLogedIn = false;

        private long? userId;
        protected override async Task OnInitializedAsync()
        {
            await fnGetUserId();
           

        }
        private async Task fnGetUserId()
        {
            var userdata = await _common.CookieClaimData();
            if (userdata.isSuccess == true)
            {
                var user = (ClaimUserData)userdata.result;
                userId = user.userid;
            }

        }
      
        private async Task fnOnValidLoginSubmit()
        {
            var result = await _loginservice.ValidateLogin(LoginModel);
            if (result.result != null && result.isSuccess)
            {
                CookieData = JsonSerializer.Deserialize<CookieData>(result.result.ToString());
                isLogedIn = true;
                if (CookieData != null && CookieData.token != null)
                {
                    var dataToken = CookieData.token;
                    objFormMessage.message = "Login successfully";
                    objFormMessage.cssClass = "alert-success";

                    string privilegejson = JsonSerializer.Serialize(CookieData.privilages);

                    var claim = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name ,CookieData.userName),
                    new Claim(ClaimTypes.Sid , CookieData.userId.ToString()),
                    new Claim("Token", CookieData.token),
                    new Claim("RoleId", CookieData.roleId.ToString()),

                    new Claim(ClaimTypes.Role, CookieData.roleName),
                    new Claim("privilege",privilegejson.ToString()),

                    };
                    var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    var aunthPro = new AuthenticationProperties() { };
                    await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
                    aunthPro);
                    objNav.NavigateTo("/", true);
                }
                else
                {
                    objFormMessage.message = "Error occured";
                    objFormMessage.cssClass = "alert-danger";
                }
            }
            else
            {
                objFormMessage.message = "Invalid UserName or Password";
                objFormMessage.cssClass = "alert-danger";
            }
        }
    }
}