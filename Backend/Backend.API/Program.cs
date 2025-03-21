using BusinessLogic;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddControllers();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();