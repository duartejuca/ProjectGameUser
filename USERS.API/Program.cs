using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using USERS.API.Data;
using USERS.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(Configuration.GetSection("ConnectionStrings")["SQLiteConn"])
    //options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["SQLServerConn"])
);

// adicionando a injeção de dependencia para configurar user manager e a classe user que herda de userIdentity
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "USERS.API v1.0.0");
        }
    );
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "API",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();