using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using EMS.Client.Pages.Employee;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Client.Pages.Department
{
    public partial class DepartmentList
    {
        [Inject] private IDepartmentService _departmentService { get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private IEnumerable<DepartmentModel> objdepartment { get; set; }
        private UpsertDepartment UpsertDepartment { get; set; }
        private string pageText = "Loading ...";
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        [Inject] private ICommonService common { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var privilege = await common.CookiesService("Employee");
            List<Task> tasks = new List<Task>()
             {
              FnDepartmentSearch()
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
        private async Task FnDepartmentSearch()
        {
            var data = await _departmentService.GetDepartment();
            if (data != null && data.isSuccess)
            {
                objdepartment = JsonSerializer.Deserialize<IEnumerable<DepartmentModel>>(data.result.ToString());

            }
            else
            {
                objdepartment = null;
                formMessage.message = GenericMessage.noRecordFound;
                formMessage.cssClass = GenericMessage.error;
            }
        }
        private async Task fnDepartmentopen(string title, DepartmentModel department)
        {
            title = "Department";
            UpsertDepartment.fnOpenModel(title,department);
        }
        private async Task fndeletedepartment(string title, DepartmentModel department)
        {

            int departmentid = department.deptId;
            var data = await _departmentService.DeleteDepartment(departmentid);
            if (data.isSuccess)
            {
                await OnInitializedAsync();
            }

        }
    }
}