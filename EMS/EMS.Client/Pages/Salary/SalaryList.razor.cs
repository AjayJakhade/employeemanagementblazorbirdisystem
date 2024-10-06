using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using EMS.Client.Pages.Employee;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Client.Pages.Salary
{
    public partial class SalaryList
    {
        [Inject] private ISalaryService _salaryService { get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private IEnumerable<SalaryModel> objsalary { get; set; }
        private string pageText = "Loading ...";
       private UpsertSalary UpsertSalary { get; set; }
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        [Inject] private ICommonService common { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var privilege = await common.CookiesService("Employee");
            List<Task> tasks = new List<Task>()
             {
              fnSalarySearch()
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
        private async Task fnSalarySearch()
        {
            var data = await _salaryService.GetSalary();
            if (data != null && data.isSuccess)
            {
                objsalary = JsonSerializer.Deserialize<IEnumerable<SalaryModel>>(data.result.ToString());

            }
            else
            {
                objsalary = null;
                formMessage.message = GenericMessage.noRecordFound;
                formMessage.cssClass = GenericMessage.error;
            }
        }
        private async Task fnOpenSalary(string title, SalaryModel salary)
        {
            title = "Salary";
           UpsertSalary.fnOpenModel(title, salary);
        }
        private async Task fndeletesalary(string title, SalaryModel salary)
        {

            int salaryid = salary.salaryId;
            var data = await _salaryService.DeleteSalary(salaryid);
            if (data.isSuccess)
            {
                await OnInitializedAsync();
            }

        }
    }
}