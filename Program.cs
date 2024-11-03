
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Project.Areas.Admin.Repositories;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Areas.Admin.Services;
using Project.Areas.Admin.Services.Interfaces;
using Project.Data.EF;
using Project.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();

builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<ISpecialityService, SpecialityService>();


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//---------------------------Подключение к БД Парсера--------------------//

builder.Services.AddDbContext<ParserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Parser"),
        new MySqlServerVersion(new Version(8, 0, 25))
    )
);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Users"),
        new MySqlServerVersion(new Version(8, 0, 25))
    )
);

//-----------------------------------------------------------------------//

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login"; // путь к странице логина
        options.AccessDeniedPath = "/Account/AccessDenied"; // путь к странице отказа в доступе
        options.Cookie.Name = "UserAuthCookie"; // имя куки
    });

builder.Services.AddAuthorization();



var app = builder.Build();


// app.UseStatusCodePagesWithReExecute("/Account/AccessDenied");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // подключение аутентификации
app.UseAuthorization(); //подключение авторизации

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
