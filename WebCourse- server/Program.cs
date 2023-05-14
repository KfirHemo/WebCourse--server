
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebCourse__server;
using WebCourse__server.Controllers;
using WebCourse__server.RepositorysAndEF.Entity;


//"Server= SARIX270\\SQLEXPRESS ;Database= WebProjectDB; MultipleActiveResultSets=true; User Id = dbcon;Integrated Security=True; Encrypt=True;";
//"Data Source=SARIX270\\SQLEXPRESS;Initial Catalog=WebProjectDB;Integrated Security=True; Encrypt=True; TrustServerCertificate=True; "

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt => opt.AllowEmptyInputInBodyModelBinding = true);
var connectionString = "Data Source=SARIX270\\SQLEXPRESS;Initial Catalog=WebProjectDB;Integrated Security=True; Encrypt=True; TrustServerCertificate=True; ";
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

