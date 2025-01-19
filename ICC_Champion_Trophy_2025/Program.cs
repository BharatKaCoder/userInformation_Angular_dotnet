using ICC_Champion_Trophy_2025;
using ICC_Champion_Trophy_2025.Repository;
using ICC_Champion_Trophy_2025.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1) DB context Injected
builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// 2) Repository file injected
builder.Services.AddHttpClient<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();

// 3) AutoMapper
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

app.UseAuthorization();

app.MapControllers();

app.Run();
