using H4App3API.Database;
using H4App3API.Repositories;
using H4App3API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var InitialRules = "_InitialRules";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: InitialRules,
	policy =>
	{
		policy.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

builder.Services.AddDbContext<ScrumContext>(
    x => x.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=h4Scrum;Trusted_Connection=True"));


builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IColumnRepository, ColumnRepository>();

builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IColumnService, ColumnService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(InitialRules);

app.UseAuthorization();

app.MapControllers();

app.Run();
