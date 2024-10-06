using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using EMS.Client.Application.Services.Users;

namespace EMS.Components.Privilege
{
    public partial class PrivilegeList
    {
        [SupplyParameterFromForm] private PrivilegeSearch PrivilegeSearch { get; set; } = new();

        [Inject] private IPrivilegeService _privilegeService { get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private IEnumerable<PrivilegeModel>? objPrivilege { get; set; }
        private string pageText = "Loading ...";
        private UpsertPrivilege UpsertPrivilege { get; set; }
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        [Inject] private ICommonService common { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var privilege = await common.CookiesService("Employee");
            List<Task> tasks = new List<Task>()
             {
              fnPrivilegeSearch()
             };
            await Task.WhenAll(tasks);
            var rolePre = (PrivilagesDtoCookie)privilege.result;
            if (rolePre != null)
            {
                permission.canAdd = rolePre.canAdd;
                permission.canEdit = rolePre.canEdit;
                permission.canGet = rolePre.canGet;
                permission.canDelete = rolePre.canDelete;
            }
        }
        private async Task fnPrivilegeSearch()
        {
            var data = await _privilegeService.GetPrivilege(PrivilegeSearch);
            if (data != null && data.isSuccess)
            {
                objPrivilege = JsonSerializer.Deserialize<IEnumerable<PrivilegeModel>>(data.result.ToString());

            }
            else
            {
                objPrivilege = null;
                formMessage.message = GenericMessage.noRecordFound;
                formMessage.cssClass = GenericMessage.error;
            }
        }
        private async Task fnOpenPrivilege(string title, PrivilegeModel privilegeModel)
        {
            title = "privilege";
            UpsertPrivilege.fnOpenModel(title, privilegeModel);
        }
        private async Task fndeleteprivilege(string title, PrivilegeModel privilegeModel)
        {

            int privilegeid = privilegeModel.privilegeid;
            var data = await _privilegeService.DeletePrivilege(privilegeid);
            if (data.isSuccess)
            {
                await OnInitializedAsync();
            }

        }
    }
}