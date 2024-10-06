using EMS.Client.Application;
using EMS.Client.BuisnessLayerShared.Abstractions;
using EMS.Client.BuisnessLayerShared.Common;
using EMS.Client.BuisnessLayerShared.Models;
using EMS.Client.Pages;
using EMS.Components;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddDependencyInjection();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "Authorization";
        opt.LoginPath = "/EMS/login";
        opt.LogoutPath = "/EMS/login";
        opt.Cookie.MaxAge = TimeSpan.FromHours(12);
        opt.AccessDeniedPath = "/access-denied";
    });
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });
var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(EMS.Client._Imports).Assembly);
app.MapPost("/api/context/getuser", async (Shared name, ICommonService method, HttpContext _context) =>
{
    BaseResponse resp = await method.CookiesService(name.name);
    return resp;
}).RequireAuthorization();

app.MapPost("/api/context/claim", async (ICommonService method, HttpContext _context) =>
{
    BaseResponse resp = await method.CookieClaimData();
    return resp;
}).RequireAuthorization();

app.MapPost("/api/GetEmployee", async (IEmployeeService method, HttpContext _context) =>
{
    BaseResponse resp = await method.GetEmployee();
    return resp;
}).RequireAuthorization();
app.MapPost("/api/CreateEmployee", async (EmployeeModel employee, IEmployeeService method, HttpContext _context) =>
{
    BaseResponse resp = await method.CreateEmployee(employee);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/DeleteEmployee", async (Shared shared,IEmployeeService method, HttpContext _context) =>
{
    int employeeid = Convert.ToInt32(shared.name);
    BaseResponse resp = await method.DeleteEmployee(employeeid);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/UpdateEmployee", async (EmployeeModel employee, IEmployeeService method, HttpContext _context) =>
{
    BaseResponse resp = await method.UpdateEmployee(employee);
    return resp;
}).RequireAuthorization();



app.MapPost("/api/GetDepartment", async (IDepartmentService method, HttpContext _context) =>
{
    BaseResponse resp = await method.GetDepartment();
    return resp;
}).RequireAuthorization();
app.MapPost("/api/CreateDepartment", async (DepartmentModel department, IDepartmentService method, HttpContext _context) =>
{
    BaseResponse resp = await method.CreateDepartment(department);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/DeleteDepartment", async (Shared shared, IDepartmentService method, HttpContext _context) =>
{
    int departmentid = Convert.ToInt32(shared.name);
    BaseResponse resp = await method.DeleteDepartment(departmentid);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/UpdateDepartment", async (DepartmentModel department, IDepartmentService method, HttpContext _context) =>
{
    BaseResponse resp = await method.UpdateDepartment(department);
    return resp;
}).RequireAuthorization();


app.MapPost("/api/GetSalary", async (ISalaryService method, HttpContext _context) =>
{
    BaseResponse resp = await method.GetSalary();
    return resp;
}).RequireAuthorization();
app.MapPost("/api/CreateSalary", async (SalaryModel salary, ISalaryService method, HttpContext _context) =>
{
    BaseResponse resp = await method.CreateSalary(salary);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/DeleteSalary", async (Shared shared, ISalaryService method, HttpContext _context) =>
{
    int salaryid = Convert.ToInt32(shared.name);
    BaseResponse resp = await method.DeleteSalary(salaryid);
    return resp;
}).RequireAuthorization();
app.MapPost("/api/UpdateSalary", async (SalaryModel salary, ISalaryService method, HttpContext _context) =>
{
    BaseResponse resp = await method.UpdateSalary(salary);
    return resp;
}).RequireAuthorization();



app.Run();
