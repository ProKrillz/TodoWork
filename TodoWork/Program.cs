using TodoWork.BLL.TodoServices;
using TodoWork.Domain.SQLConnection;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddRazorPages().Services.AddSingleton<IConnection>(new Connection(connectionString))
.AddRazorPages().Services.AddSingleton<ITodoServices, TodoServices>()
.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.UseStatusCodePagesWithRedirects("/Error/SiteDoesntExist");
app.Run();