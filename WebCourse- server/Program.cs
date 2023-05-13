
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebCourse__server;
using WebCourse__server.Controllers;
using WebCourse__server.Entitys;

var context = new ServerContext();
var userRepo = new UserRepo(context);

//await userRepo.AddUser("Kfir", "1234", "kfir@gmail.com", "Manager");
//var user = userRepo.GetUser("Kfir");
//var check =await userRepo.RemoveUser("Kfir");

//var grades = userRepo.GetStudentGrade("Kfir");

//await userRepo.AddCourse("Math");
//var result = userRepo.GetCourse("Math");
//var result = userRepo.GetCourses();

//await userRepo.AddGrade("Kfir", "Math", 100);

//Console.WriteLine( );



//await userRepo.AddGrade("Kfir", "Math", 100);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ServerContext>(options =>
    options.UseSqlServer());

builder.Services.AddScoped<UsersController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
