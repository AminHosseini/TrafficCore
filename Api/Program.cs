var builder = WebApplication.CreateBuilder(args);

Settings.Configuration = builder.Configuration;

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

AccountBootstrapper.Configure(builder.Services);
UserBootstrapper.Configure(builder.Services);

builder.Services.AddKeycloakAuthentication(Settings.Configuration);
builder.Services.AddSwaggerApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
