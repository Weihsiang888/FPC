using FPC.Components;
using FPC.Data;
using FPC.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using CommonLibraryP.LogPKG;
using CommonLibraryP.MachinePKG; // 新增此 using

var webApOpts = new WebApplicationOptions
{
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ?
        AppContext.BaseDirectory : default,
    Args = args
};

var builder = WebApplication.CreateBuilder(webApOpts);
builder.Host.UseWindowsService();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDevExpressBlazor(options => {
    options.SizeMode = DevExpress.Blazor.SizeMode.Large;
});

builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "V1" });
});

// 註冊 SerilogService (新增此行)
builder.Services.AddSingleton<SerilogService>(sp => 
    new SerilogService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<UIService>();
builder.AddMachineService();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<DxThemesService>();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContextFactory<DSDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AllowAnonymous();

app.Run();