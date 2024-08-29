using BackEnd.Data;
using BackEnd.Data.Repositorios;
using BackEnd.Interface;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRepoRolUsuario, RepoRolUsuario>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoSeguidor, RepoSeguidor>();
builder.Services.AddScoped<IRepoPost, RepoPost>();
builder.Services.AddScoped<IRepoUsuarioLikes, RepoUsuarioLikes>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var repoUsuario = services.GetRequiredService<IRepoUsuario>();
    var repoRolUsuario = services.GetRequiredService<IRepoRolUsuario>();
    SeedData.Initialize(repoUsuario, repoRolUsuario);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
