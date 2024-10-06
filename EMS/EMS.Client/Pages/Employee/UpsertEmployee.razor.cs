using EMS.Client.Application;
using EMS.Client.Application.Services.Privilege;
using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Client.Pages.Employee
{
    public partial class UpsertEmployee
    {
        [SupplyParameterFromForm] private EmployeeModel EmployeeModel { get; set; } = new();
        [Inject] private IEmployeeService _employeeService { get; set; }
        [Inject] private IDepartmentService _departmentService {  get; set; }
        [Inject] private ICommonService _common { get; set; }
        private IEnumerable<DepartmentModel> objDepartment {  get; set; }
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }
        private FormMessage FormMessage { get; set; } = new();
        private string frmHeading { get; set; } = "Privilege";
        private string ModalDisplay { get; set; } = "none;";
        private string ModalTitle { get; set; }
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        [Inject] private CallServices CallServices { get; set; }
        public int EmployeeId { get; set; }
        private async Task fnCloseModal()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            if (FormMessage != null && FormMessage.cssClass == "alert-success")
            {
                await onDoneButtonClicked.InvokeAsync();
            }

            StateHasChanged();
        }
        private async Task LoadDropDown()
        {
            List<Task> tasks = new List<Task>()
           {
               fnGetDepartment()

           };
            await Task.WhenAll(tasks);
        }
        public async Task fnOpenModel(string Title, EmployeeModel employeeModel)
        {
            ModalTitle = Title;
            EmployeeId = employeeModel.empId;
            EmployeeModel = new EmployeeModel
            {
               empId=employeeModel.empId,
               firstName=employeeModel.firstName,
               lastName=employeeModel.lastName,
               email=employeeModel.email,
               phone=employeeModel.phone,
               position=employeeModel.position,
               deptId=employeeModel.deptId,
                isActive = employeeModel.isActive

            };

            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            FormMessage = new();
            await LoadDropDown();
            StateHasChanged();
        }

        private async Task fnGetDepartment()
        {
            var result = await _departmentService.GetDepartment();
            if(result !=null && result.isSuccess)
            {
                objDepartment = JsonSerializer.Deserialize<IEnumerable<DepartmentModel>>(result.result.ToString());
            }
            else
            {
              FormMessage.message += "Department - Not Found ";
            }
            
        }
        private async Task fnEmployeeSave()
        {


            if (EmployeeId > 0)
            {
                var result = await _employeeService.UpdateEmployee(EmployeeModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.updateSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    EmployeeModel = new();
                }
                else
                {
                    FormMessage.message = GenericMessage.saveErrorMessage;
                    FormMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {
                var result = await _employeeService.CreateEmployee(EmployeeModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.addSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    EmployeeModel = new();
                }
                else
                {
                    FormMessage.message = GenericMessage.saveErrorMessage;
                    FormMessage.cssClass = GenericMessage.error;
                }
            }


        }

    }
}