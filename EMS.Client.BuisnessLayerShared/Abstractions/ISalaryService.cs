using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.BuisnessLayerShared.Abstractions
{
    public interface ISalaryService
    {
        Task<BaseResponse> GetSalary();
        Task<BaseResponse> CreateSalary(SalaryModel salaryModel);
        Task<BaseResponse> UpdateSalary(SalaryModel salaryModel);
        Task<BaseResponse> DeleteSalary(int salaryid);
    }
}
