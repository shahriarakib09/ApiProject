using ApiProject.Features; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(name: "ReactJSDomain",
            policy => policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod()
            );
    }
    );
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ReactJSDomain");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
