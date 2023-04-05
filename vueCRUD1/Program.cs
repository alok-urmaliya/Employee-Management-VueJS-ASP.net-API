using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using vueCRUD1.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<vueCRUD1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("vueCRUD1Context") ?? throw new InvalidOperationException("Connection string 'vueCRUD1Context' not found.")));


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();