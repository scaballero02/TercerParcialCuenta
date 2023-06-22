using Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Infraestructure.Repository;
using Microsoft.Extensions.Configuration;
using Infraestructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TercerParcialCuenta;

namespace TercerParcialCuenta;

public class Startup
{
    public static WebApplication InitializeApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        ConfigureServices(builder, configuration);
        var app = builder.Build();
        Configure(app);
        return app;
    }

    private static void ConfigureServices(WebApplicationBuilder builder, IConfiguration configuration)
    {

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Agrega tus servicios personalizados aquí
        builder.Services.AddScoped<OperacionService>(); 
        builder.Services.AddScoped<CuentaRepository>(provider =>
        {
            var connectionString = configuration.GetConnectionString("postgresDB");
            return new CuentaRepository(connectionString);
        });


        builder.Services.AddSingleton(builder.Configuration.GetSection("ConnectionStrings"));

        var key = Encoding.ASCII.GetBytes("E@!knadkjbad45678ad.ci@456akjd|!45a");


        builder.Services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
        ).AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false
            };
        });



    }
    private static void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();
    }


}
