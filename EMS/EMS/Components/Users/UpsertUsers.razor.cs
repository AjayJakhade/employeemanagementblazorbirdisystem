using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Common;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Components.Users
{
    public partial class UpsertUsers
    {
        [SupplyParameterFromForm] private UsersModel UserModel { get; set; } = new();
        [Inject] private IUserService _UsersService { get; set; }
        [Inject] private IRoleService _roleservice { get; set; }
     
       
        private IEnumerable<Roles> objRole { get; set; }
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }

        private string frmHeading { get; set; } = "Users";

        private string ModalDisplay { get; set; } = "none;";
        private string ModalTitle { get; set; }
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        private FormMessage formMessage { get; set; } = new();
        public long UserId { get; set; }
        private async Task fnCloseModal()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            if (formMessage is not null && formMessage.cssClass == "alert-success")
            {
                await onDoneButtonClicked.InvokeAsync();
            }

            StateHasChanged();
        }
        private async Task LoadDropDown()
        {
            List<Task> tasks = new List<Task>()
           {
               fnGetRole()
             
           };
            await Task.WhenAll(tasks);
        }
       
        private async Task fnGetRole()
        {
            var result = await _roleservice.GetRoles();
            if (result != null && result.isSuccess)
            {
                objRole = JsonSerializer.Deserialize<IEnumerable<Roles>>(result.result.ToString());
            }
            else
            {
                formMessage.message += "Role - Not Found ";
            }
        }
        public async Task fnOpenUsersModel(string title, UsersModel users)
        {
            title = "Users";
            UserId = users.userid;
            UserModel = new UsersModel()
            {
                userid = users.userid,
                username = users.username,
                email = users.email,
                mobile = users.mobile,
                password = users.password,
                roleid = users.roleid,
                isActive = users.isActive

            };
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            formMessage = new();

            await LoadDropDown();

            StateHasChanged();
        }
        private async Task fnSaveUsers()
        {
            if (UserId > 0)
            {
                var result = await _UsersService.UpdateUser(UserModel);
                if (result != null && result.isSuccess)
                {

                    formMessage.message = GenericMessage.addSuccess;
                    formMessage.cssClass = GenericMessage.success;
                    UserModel = new();
                }
                else
                {
                    formMessage.message = GenericMessage.saveErrorMessage;
                    formMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {


                var result = await _UsersService.CreateUser(UserModel);
                if (result != null && result.isSuccess)
                {

                    formMessage.message = GenericMessage.addSuccess;
                    formMessage.cssClass = GenericMessage.success;
                    UserModel = new();
                }
                else
                {
                    formMessage.message = GenericMessage.saveErrorMessage;
                    formMessage.cssClass = GenericMessage.error;
                }
            }
        }
    }
}