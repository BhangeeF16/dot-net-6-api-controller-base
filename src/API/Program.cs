using API.Extensions;
using Application;
using Application.Pipeline.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.UseSwagger(builder.Environment);

builder.Services.InjectDependencies(builder.Configuration);
builder.Services.AddHealthChecks();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*", "http://localhost:3011", "https://localhost:3011", "https://localhost:7261").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

//app.UseExceptionHandler("/Error");

//HTTP Strict Transport Security : Method used by server to declare that they should only be accessed using HTTPS (secure connection) only
app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors("corsapp");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddlewares();
app.UseHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();

app.MapSwagger();

app.MapControllers();

app.Run();
