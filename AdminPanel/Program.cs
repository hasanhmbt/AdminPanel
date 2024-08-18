using AdminPanel;
using AdminPanel.Data;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<AdminPanelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdminPanelContext") ?? throw new InvalidOperationException("Connection string 'AdminPanelContext' not found.")));

builder.Services.AddControllersWithViews();

// Configure ELMAH.io
builder.Services.AddElmahIo(o =>
{
    o.ApiKey = builder.Configuration["ElmahIo:ApiKey"];
    o.LogId = new Guid(builder.Configuration["ElmahIo:LogId"]);
});

builder.Services.AddElmah<SqlErrorLog>(options =>
{
    options.Path = "elmah"; // URL will be /elmah for logs
    options.ConnectionString = builder.Configuration.GetConnectionString("AdminPanelContext");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();


// Use ELMAH.io middleware
app.UseMiddleware<ElmahLoggingMiddleware>();
app.UseElmahIo();
app.UseElmah();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
