
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebCourse__server;
using WebCourse__server.Controllers;
using WebCourse__server.RepositorysAndEF.Entity;

//var context = new ServerContext();
//var userRepo = new UserRepo(context);

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
builder.Services.AddControllers(opt => opt.AllowEmptyInputInBodyModelBinding = true);

var connectionString = "Server= ;Database=;MultipleActiveResultSets=true; User Id =; password=";

// SQLUser with login

builder.Services.AddDbContext<ServerContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<UserRepo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection(); 
app.UseAuthentication();
app.MapControllers();
app.Run();

