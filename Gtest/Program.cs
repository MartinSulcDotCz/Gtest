using Gtest.Common.Services;
using Gtest.Common.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICountryGwpService, CountryGwpService>();

builder.Services.AddSwaggerGen(sa =>
{
    sa.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gtest API",
        Version = "v1",
        Description = "For fun :-)"
    });
    // TODO MSU sa.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Gtest.xml"));

    // TODO MSU sa.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
    //sa.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});
    //sa.AddSecurityRequirement(new OpenApiSecurityRequirement
    //    {
    //        {
    //            new OpenApiSecurityScheme
    //            {
    //                Reference = new OpenApiReference
    //                {
    //                    Type = ReferenceType.SecurityScheme,
    //                    Id = "Bearer"
    //                }
    //            },
    //        Array.Empty<string>()
    //        }
    //});
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lumnio API v1"); // http://localhost:9091/swagger/index.html
        c.DocExpansion(DocExpansion.None);
    });
    // TODO MSU _ = app.UseExceptionHandler("/api/error-development");
}
// TODO MSU else
//{
//    _ = app.UseExceptionHandler("/api/error");
//}

_ = app.UseHttpsRedirection()
    .UseAuthorization();
_ = app.MapControllers();

app.Run();
