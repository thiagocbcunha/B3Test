using B3.Test.Crosscuting.Ioc;
using B3.Test.Library.Tracing;
using B3.Test.Library.Logging;
using B3.Test.Library.Security;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEnterpriseSecurity();
builder.Services.AddEndpointsApiExplorer();
builder.Logging.ConfigureEnterpriceLog(builder.Configuration, "ApplicationName");
builder.Services.AddCors(options => options.AddPolicy("FreeAccessPolicy", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var enterpriseTracingBuilder = builder.Services.CreateEnterpriseTracingBuilder(builder.Configuration);
enterpriseTracingBuilder.AddSQLInstrumentation();
enterpriseTracingBuilder.BuildService();

builder.Services.B3AppConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || environmentName.Equals("docker", StringComparison.InvariantCultureIgnoreCase))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors();

await app.RunAsync();