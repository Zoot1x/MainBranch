using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Project.Data.EF;
using Project.Repositories;
using Project.Repositories.Interfaces;
using Project.Services;
using Project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//Настройка логов


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ParserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Parser"))
);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Репозитории
builder.Services.AddScoped<ISpecialityRepository, SpecialityRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();

//Сервисы
builder.Services.AddScoped<ISpecialityService, SpecialityService>();
builder.Services.AddScoped<IDisciplineService, DisciplineService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
