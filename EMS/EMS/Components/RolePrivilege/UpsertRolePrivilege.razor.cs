using EMS.Client.Application.Services.RolePrivilege;
using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using EMS.Client.BuisnessLayer.Models;

namespace EMS.Components.RolePrivilege
{
    public partial class UpsertRolePrivilege
    {

        [SupplyParameterFromForm] private RolePrivilegeModel RolePrivilegeModel { get; set; } = new();
        [Inject] private IRolePrivilege _roleprivilege { get; set; }
      
        [Inject] private IRoleService _roleService { get; set; }
        [Inject] private IPrivilegeService _privilegeservice { get; set; }
        private IEnumerable<PrivilegeModel> objprivilege { get; set; }
        private IEnumerable<Roles> objRole { get; set; }
       
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private string frmHeading { get; set; } = "RolePrivilege";
        private string ModalDisplay { get; set; } = "none";
        private string? ModalTitle { get; set; } = "RolePrivilege";
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        public long roleprivilegeid {  get; set; }

        private async Task fnCloseModal()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            if (frmMessage is not null && frmMessage.cssClass == "alert-success")
            {
                await onDoneButtonClicked.InvokeAsync();
            }

            StateHasChanged();
        }
        private async Task LoadDropDown()
        {
            List<Task> tasks = new List<Task>()
            {
                fnRole(),
                fnPrivilege()
            };
            await Task.WhenAll(tasks);

        }
        public async Task OpenRolePrivilege(string title, RolePrivilegeModel rolePrivilege)
        {
            title = "RolePrivilege";
            roleprivilegeid = rolePrivilege.roleprivilegeid;
            RolePrivilegeModel = new RolePrivilegeModel
            {
                roleprivilegeid = rolePrivilege.roleprivilegeid,
                roleid = rolePrivilege.roleid,
                privilegeid = rolePrivilege.privilegeid,
              
                canadd = rolePrivilege.canadd,
                canget = rolePrivilege.canget,
                candelete = rolePrivilege.candelete,
                canedit = rolePrivilege.canedit,
                canexport = rolePrivilege.canexport,
                isActive = rolePrivilege.isActive
            };

            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            frmMessage = new();
            await LoadDropDown();

            StateHasChanged();

        }
        private async Task fnRole()
        {
            var result = await _roleService.GetRoles();
            if (result != null && result.isSuccess)
            {
                objRole = JsonSerializer.Deserialize<IEnumerable<Roles>>(result.result.ToString());

            }

        }
        private async Task fnPrivilege()
        {
            var result = await _privilegeservice.GetPrivilege(new PrivilegeSearch { IsActive = true });
            if (result.isSuccess && result != null)
            {
                objprivilege = JsonSerializer.Deserialize<IEnumerable<PrivilegeModel>>(result.result.ToString());
            }

        }
      
        private async Task fnSaveRolePrivilege()
        {
            if (roleprivilegeid > 0)
            {
                var result = await _roleprivilege.UpdateRolePrivilege(RolePrivilegeModel);
                if (result.isSuccess && result != null)
                {
                    frmMessage.message = GenericMessage.addSuccess;
                    frmMessage.cssClass = GenericMessage.success;
                    RolePrivilegeModel = new();
                }
                else
                {
                    frmMessage.message = GenericMessage.saveErrorMessage;
                    frmMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {
                var result = await _roleprivilege.CreateRolePrivilege(RolePrivilegeModel);
                if (result.isSuccess && result != null)
                {
                    frmMessage.message = GenericMessage.addSuccess;
                    frmMessage.cssClass = GenericMessage.success;
                    RolePrivilegeModel = new();
                }
                else
                {
                    frmMessage.message = GenericMessage.saveErrorMessage;
                    frmMessage.cssClass = GenericMessage.error;
                }
            }
           
        }
    }
}