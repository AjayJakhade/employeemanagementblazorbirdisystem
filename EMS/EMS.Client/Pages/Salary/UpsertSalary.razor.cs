using EMS.Client.Application;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Client.Pages.Salary
{
    public partial class UpsertSalary
    {
        [SupplyParameterFromForm] private SalaryModel SalaryModel { get; set; } = new();
        [Inject] private IEmployeeService _employeeService { get; set; }
        [Inject] private ISalaryService _salaryService { get; set; }
        [Inject] private ICommonService _common { get; set; }
        private IEnumerable<SalaryModel> objsalary { get; set; }
        private IEnumerable<EmployeeModel> objEMployee {  get; set; }
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }
        private FormMessage FormMessage { get; set; } = new();
        private string frmHeading { get; set; } = "Privilege";
        private string ModalDisplay { get; set; } = "none;";
        private string ModalTitle { get; set; }
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        [Inject] private CallServices CallServices { get; set; }
        public int SalaryId { get; set; }
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
               fnGetEmployee()

           };
            await Task.WhenAll(tasks);
        }
        public async Task fnOpenModel(string Title, SalaryModel salary)
        {
            ModalTitle = Title;
            SalaryId = salary.salaryId;
            SalaryModel = new SalaryModel
            {
                salaryId=salary.salaryId,
                empId = salary.empId,
                amount=salary.amount,
               
                isActive = salary.isActive

            };

            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            FormMessage = new();
            await LoadDropDown();
            StateHasChanged();
        }

        private async Task fnGetEmployee()
        {
            var result = await _employeeService.GetEmployee();
            if (result != null && result.isSuccess)
            {
                objEMployee = JsonSerializer.Deserialize<IEnumerable<EmployeeModel>>(result.result.ToString());
            }
            else
            {
                FormMessage.message += "Employee - Not Found ";
            }

        }
        private async Task fnSalarySave()
        {


            if (SalaryId > 0)
            {
                var result = await _salaryService.UpdateSalary(SalaryModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.updateSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    SalaryModel = new();
                }
                else
                {
                    FormMessage.message = GenericMessage.saveErrorMessage;
                    FormMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {
                var result = await _salaryService.CreateSalary(SalaryModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.addSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    SalaryModel = new();
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