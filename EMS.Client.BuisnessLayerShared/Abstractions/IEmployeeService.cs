using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Abstractions
{
    public interface IEmployeeService
    {
        Task<BaseResponse> GetEmployee();
        Task<BaseResponse> CreateEmployee(EmployeeModel employeeModel);
        Task<BaseResponse> UpdateEmployee(EmployeeModel employeeModel);
        Task<BaseResponse> DeleteEmployee(int employeeid);

    }
}
