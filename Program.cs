using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Project.Areas.Admin.Repositories;
using Project.Areas.Admin.Repositories.Interfaces;
using Project.Areas.Admin.Services;
using Project.Areas.Admin.Services.Interfaces;
using Project.Data.EF;
using Project.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//---------------------------Подключение к БД Парсера--------------------//

builder.Services.AddDbContext<ParserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Parser"),
        new MySqlServerVersion(new Version(8, 0, 25))
    )
);

//-----------------------------------------------------------------------//

//-------------------Работа с Idenity-------------------------//

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Users"),
        new MySqlServerVersion(new Version(8, 0, 25))
    )
);

builder
    .Services.AddIdentity<User, IdentityRole>(opts =>
    {
        opts.Password.RequiredLength = 5; // минимальная длина
        opts.Password.RequireNonAlphanumeric = false; // требуются ли не алфавитно-цифровые символы
        opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
        opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
        opts.Password.RequireDigit = false; // требуются ли цифры
        opts.User.RequireUniqueEmail = false; // уникальный email
        opts.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_"; // допустимые символы
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>();

//------------------------------------------------------------------------//

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//------------------------------Репозитории---------------------------//
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();

//------------------------------------------------------------------------//

//-----------------------------------Сервисы------------------------//
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IDisciplineService, DisciplineService>();

//------------------------------------------------------------------------//


//---------------------Роли--------------------------//


//-----------------------------------------------------//

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // подключение аутентификации
app.UseAuthorization(); //подключение авторизации

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
