using EMS.Client.Application.Services.Privilege;
using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using EMS.Client.Proxy.Employee;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Text.Json;

namespace EMS.Client.Pages.Employee
{
    public partial class EmployeeList
    {
       

        [Inject] private IEmployeeService _employeeService { get; set; }
        private FormMessage frmMessage { get; set; } = new();
        private IEnumerable<EmployeeModel> objemployee { get; set; }
        private string pageText = "Loading ...";
        private UpsertEmployee UpsertEmployee {  get; set; }
        private FormMessage formMessage { get; set; } = new();
        PaginationState pagination = new PaginationState { ItemsPerPage = 25 };
        private PrivilagesDtoCookie permission { get; set; } = new PrivilagesDtoCookie();
        [Inject] private ICommonService common { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var privilege = await common.CookiesService("Employee");
            List<Task> tasks = new List<Task>()
             {
              FnEmployeeSearch()
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
        private async Task FnEmployeeSearch()
        {
            var data = await _employeeService.GetEmployee();
            if (data != null && data.isSuccess)
            {
                objemployee = JsonSerializer.Deserialize<IEnumerable<EmployeeModel>>(data.result.ToString());

            }
            else
            {
                objemployee = null;
                formMessage.message = GenericMessage.noRecordFound;
                formMessage.cssClass = GenericMessage.error;
            }
        }
        private async Task fnOpenEmployee(string title, EmployeeModel employee)
        {
            title = "Employee";
            UpsertEmployee.fnOpenModel(title, employee);
        }
        private async Task fndeleteemployee(string title, EmployeeModel employee)
        {

            int employeeid = employee.empId;
            var data = await _employeeService.DeleteEmployee(employeeid);
            if (data.isSuccess)
            {
                await OnInitializedAsync();
            }

        }

    }
}