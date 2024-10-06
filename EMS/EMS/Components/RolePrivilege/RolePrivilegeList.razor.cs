using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using EMS.Client.Application.Services.Users;

namespace EMS.Components.RolePrivilege
{
    public partial class RolePrivilegeList
    {
        [Inject] private IRolePrivilege _RolePrivilegeService { get; set; }
        [Inject] private IRoleService _roleservice { get; set; }
        [Inject] private IPrivilegeService _privilegeservice {  get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private IEnumerable<RolePrivilegeModel> objRolePrivilege { get; set; }
        private UpsertRolePrivilege UpsertRolePrivilege { get; set; }
        private IEnumerable<Roles> objRole { get; set; }
        private string pageText = "Loading ...";
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        [Inject] private ICommonService common { get; set; }
        protected override async Task OnInitializedAsync()
        {
            List<Task> tasks = new List<Task>()
            {
                 fnRolePrivilegeSearch(),
                 fnRole()
           };
            await Task.WhenAll(tasks);


            var privilege = await common.CookiesService("Employee");
            var rolePre = (PrivilagesDtoCookie)privilege.result;
            if (rolePre != null)
            {
                permission.canAdd = rolePre.canAdd;
                permission.canEdit = rolePre.canEdit;
                permission.canGet = rolePre.canGet;
                permission.canDelete = rolePre.canDelete;
            }
        }

        private async Task fnRole()
        {
            var data = await _roleservice.GetRoles();
            if (data.result != null && data.isSuccess)
            {
                objRole = JsonSerializer.Deserialize<IEnumerable<Roles>>(data.result.ToString());
            }
            else
            {
                frmMessage.message = "Role- Not Found";


            }

        }
        public async Task fnCallModal(RolePrivilegeModel roleprivilege, string ModalTitle)
        {
            UpsertRolePrivilege.OpenRolePrivilege(ModalTitle, roleprivilege);
           
        }
        public async Task fndeletemodel(RolePrivilegeModel roleprivilege, string ModalTitle)
        {

            long roleprivilegeid = roleprivilege.roleprivilegeid;
            var data = await _RolePrivilegeService.DeleteRolePrivilege(roleprivilegeid);
            if (data.isSuccess)
            {
                await OnInitializedAsync();
            }
        }
        private async Task fnRolePrivilegeSearch()
        {
            var data = await _RolePrivilegeService.GetRolePrivilege();
            if (data.result != null && data.isSuccess)
            {
                objRolePrivilege = JsonSerializer.Deserialize<IEnumerable<RolePrivilegeModel>>(data.result.ToString());
            

            }
            else
            {
                objRolePrivilege = null;
                formMessage.message = GenericMessage.noRecordFound;
                formMessage.cssClass = GenericMessage.error;
            }
        }
    }
}