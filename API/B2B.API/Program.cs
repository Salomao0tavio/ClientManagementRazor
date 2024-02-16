using B2B.API.DI;
using B2B.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configuration for the Api
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwagger();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Middleware for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// General configurations
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Route configuration and default page
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
