using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Text.Json;

namespace EMS.Components.Users
{
    public partial class UsersList
    {
        [SupplyParameterFromForm] private UserSearch UserSearch { get; set; } = new();
        [Inject] private IUserService _userService { get; set; }
       
        [Inject] private NavigationManager objNav { get; set; }
        private string pageText = "Loading...";
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        private UpsertUsers modelUpsertUsers { get; set; }
        [Inject] private ICommonService _commonService { get; set; }
        private IEnumerable<UsersModel> objUsers { get; set; }
        protected override async Task OnInitializedAsync()
        {
           
            await fnSearchUsers();

            var privilege = await _commonService.CookiesService("Employee");
            var rolePre = (PrivilagesDtoCookie)privilege.result;
            if (rolePre != null)
            {
                permission.canAdd = rolePre.canAdd;
                permission.canEdit = rolePre.canEdit;
                permission.canGet = rolePre.canGet;
                permission.canDelete = rolePre.canDelete;
            }
        }
        private async Task fnSearchUsers()
        {

            var data = await _userService.GetUser(UserSearch);
            if (data != null && data.isSuccess)
            {

                objUsers = JsonSerializer.Deserialize<IEnumerable<UsersModel>>(data.result.ToString());

            }
            else
            {
                formMessage.message = "Users Not Found";
            }
        }
       
        public async Task fnCallModal(UsersModel users, string ModalTitle)
        {
            modelUpsertUsers.fnOpenUsersModel(ModalTitle, users);
            //objNav.NavigateTo("/account/upsertbroker", true);
        }
        public async Task fnDeleteModal(UsersModel users, string ModalTitle)
        {
            long userId=users.userid;
            var data = await _userService.DeleteUser(userId);
            if(data.isSuccess)
            {
                await OnInitializedAsync();
            }
            

        }

    }
}