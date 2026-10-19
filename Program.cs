using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Services;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAcademyService, AcademyService>();
builder.Services.AddScoped<IPractitionerService, PractitionerService>();
builder.Services.AddScoped<ITechniqueService, TechniqueService>();
builder.Services.AddScoped<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddDefaultIdentity<ApplicationUser>
    (options =>{ options.SignIn.RequireConfirmedAccount = false; options.User.RequireUniqueEmail = true;})
    .AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
