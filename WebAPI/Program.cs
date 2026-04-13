using Infrastructure.DataAccess.Concrete.EntityFramework;
using Infrastructure.DataAccess.ImportDataViaApi;
using Infrastructure.DependencyResolvers.DependencyResolver;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// EF Core
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Infrastructure Services (DAL + Business Services)
builder.Services.AddInfrastructureServices();


var app = builder.Build();
 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
