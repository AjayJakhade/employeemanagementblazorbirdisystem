using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayer.Models;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using EMS.Client.Application;

namespace EMS.Components.Privilege
{
    public partial class UpsertPrivilege
    {
        [SupplyParameterFromForm] private PrivilegeModel PrivilegeModel { get; set; } = new();
        [Inject] private IPrivilegeService _PrivilegeService { get; set; }
       
        [Inject] private ICommonService _common { get; set; }
      
        [Parameter] public EventCallback onDoneButtonClicked { get; set; }
        private FormMessage FormMessage { get; set; } = new();
        private string frmHeading { get; set; } = "Privilege";
        private string ModalDisplay { get; set; } = "none;";
        private string ModalTitle { get; set; }
        private string ModalClass { get; set; } = "";
        private bool ShowBackdrop { get; set; } = false;
        [Inject] private CallServices CallServices { get; set; }
        public int PrivilegeId {  get; set; }
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
      
      
        public async Task fnOpenModel(string Title, PrivilegeModel privilegeModel)
        {
            ModalTitle = Title;
            PrivilegeId = privilegeModel.privilegeid;
            PrivilegeModel = new PrivilegeModel
            {
                privilegeid = privilegeModel.privilegeid,
                privilegename = privilegeModel.privilegename,
               
                isActive = privilegeModel.isActive

            };

            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            FormMessage = new();
           
            StateHasChanged();
        }
        private async Task fnPrivilegeSave()
        {


            if (PrivilegeId > 0)
            {
                var result = await _PrivilegeService.UpdatePrivilege(PrivilegeModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.updateSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    PrivilegeModel = new();
                }
                else
                {
                    FormMessage.message = GenericMessage.saveErrorMessage;
                    FormMessage.cssClass = GenericMessage.error;
                }

            }
            else
            {
                var result = await _PrivilegeService.CreatePrivilege(PrivilegeModel);
                if (result != null && result.isSuccess)
                {
                    FormMessage.message = GenericMessage.addSuccess;
                    FormMessage.cssClass = GenericMessage.success;
                    PrivilegeModel = new();
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