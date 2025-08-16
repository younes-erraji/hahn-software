using HahnSoftware.Infrastructure.Persistence;
using HahnSofware.API.DependencyInjection;

using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterOpenApiSwagger();
builder.Services.InfrastructureRegistration(builder.Configuration);
builder.Services.ApplicationRegistration();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApiSwagger();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    HahnSoftwareDbContext context = scope.ServiceProvider.GetRequiredService<HahnSoftwareDbContext>();
    context.Database.Migrate();
}

app.UseStaticFiles();

app.UseCors(x => x
   // .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader()
   .SetIsOriginAllowed(origin => true)
   .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
