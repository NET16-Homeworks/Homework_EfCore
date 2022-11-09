using Homework_EfCore.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute("default",
                       "{controller=Home}/{action=Index}/{id?}");


app.Run();