using EMS.Client.Application;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace EMS.Client.Pages.Department
{
    public partial class UpsertDepartment
    {
        [SupplyParameterFromForm] private DepartmentModel DepartmentModel { get; set; } = new();
        [Inject] private IDepartmentService _departmentService { get; set; }
        [Inject] private ICommonService _common { get; set; }
        private IEnumerable<DepartmentModel> objDepartment { get; set; }
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }
        private FormMessage FormMessage { get; set; } = new();
        private string frmHeading { get; set; } = "Privilege";
        private string ModalDisplay { get; set; } = "none;";
        private string ModalTitle { get; set; }
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        [Inject] private CallServices CallServices { get; set; }
        public int DepartmentId { get; set; }
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
      
        public async Task fnOpenModel(string Title, DepartmentModel departmentModel)
        {
            ModalTitle = Title;
            DepartmentId = departmentModel.deptId;
            DepartmentModel = new DepartmentModel
            {
                
                deptId = departmentModel.deptId,
                name =departmentModel.name,
                description=departmentModel.description,
                isActive = departmentModel.isActive

            };

            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            FormMessage = new();
           
            StateHasChanged();
        }

      
        private async Task fnDepartmentSave()
        {


            if (DepartmentId > 0)
            {
                var result = await _departmentService.UpdateDepartment(DepartmentModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.updateSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    DepartmentModel = new();
                }
                else
                {
                    FormMessage.message = GenericMessage.saveErrorMessage;
                    FormMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {
                var result = await _departmentService.CreateDepartment(DepartmentModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.addSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    DepartmentModel = new();
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