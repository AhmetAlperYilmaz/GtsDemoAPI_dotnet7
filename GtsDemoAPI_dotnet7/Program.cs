using GtsDemoAPI_dotnet7;
using GtsDemoAPI_dotnet7.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDemoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Implemented Middleware, it works, it returns Unauthorized when below code is uncommented. However, there is no Authorize button to Basic Authorization
//app.UseMiddleware<BasicAuthHandler>("Member");

app.UseAuthorization();

app.MapControllers();

app.Run();
