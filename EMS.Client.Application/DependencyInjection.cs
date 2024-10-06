using EMS.Client.Application.Services.Department;
using EMS.Client.Application.Services.Employee;
using EMS.Client.Application.Services.Login;
using EMS.Client.Application.Services.Privilege;
using EMS.Client.Application.Services.RolePrivilege;
using EMS.Client.Application.Services.Roles;
using EMS.Client.Application.Services.Salary;
using EMS.Client.Application.Services.Users;
using EMS.Client.BuisnessLayer.Abstraction;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Client.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddScoped<CallServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();
            services.AddScoped<IRolePrivilege, RolePrivilegeService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRoleService, RolesService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<CommonService>();

            return services;
        }
    }
}
