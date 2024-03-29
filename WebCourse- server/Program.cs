
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebCourse__server;
using WebCourse__server.Controllers;
using WebCourse__server.RepositorysAndEF.Entity;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt => opt.AllowEmptyInputInBodyModelBinding = true);
var connectionString = "Data Source=webcourse-serverdbserver.database.windows.net;Initial Catalog=WebCourse-server_db;User ID=Kfir;Password=Aa123456;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False ";
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
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


//var host = new WebHostBuilder()
//    .UseKestrel()
//    .UseUrls("http://172.20.10.7:7187", "https://172.20.10.7:7188")
//    .Configure(app =>
//    {
//        app.Run(async context =>
//        {
//            await context.Response.WriteAsync("Hello from the server!");
//        });
//    })
//    .Build();

//host.Run();

app.Run();
