using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Abstractions
{
    public interface IDepartmentService
    {
        Task<BaseResponse> GetDepartment();
        Task<BaseResponse> CreateDepartment(DepartmentModel departmentModel);
        Task<BaseResponse> UpdateDepartment(DepartmentModel departmentModel);
        Task<BaseResponse> DeleteDepartment(int departmentid);
    }
}
