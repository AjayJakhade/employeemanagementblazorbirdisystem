using EMS.Client.Application;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.Proxy.CommonProxy;
using EMS.Client.Proxy.Department;
using EMS.Client.Proxy.Employee;
using EMS.Client.Proxy.Salary;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<ICommonService, CommonProxy>();
builder.Services.AddScoped<CallServices>();
builder.Services.AddScoped<IEmployeeService, EmployeeProxy>();
builder.Services.AddScoped<IDepartmentService, DepartmentProxy>();
builder.Services.AddScoped<ISalaryService, SalaryProxy>();
await builder.Build().RunAsync();
