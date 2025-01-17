using Microsoft.EntityFrameworkCore;
using userInformation_Angular_dotnet;
using userInformation_Angular_dotnet.Repository;
using userInformation_Angular_dotnet.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// This is CORS DI
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// Injecting reposistory here.
builder.Services.AddHttpClient<IRegistration, RegistartionRepo>();
builder.Services.AddScoped<IRegistration, RegistartionRepo>();

builder.Services.AddHttpClient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Injecting AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
