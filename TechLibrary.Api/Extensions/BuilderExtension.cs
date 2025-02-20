using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Api.Filters;
using TechLibrary.Api.Infraestructure.Data;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Api.UseCases.Books.Filters;
using TechLibrary.Api.UseCases.Checkouts;
using TechLibrary.Api.UseCases.Login;
using TechLibrary.Api.UseCases.Register;

namespace TechLibrary.Api.Extensions;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

        ApiConfiguration.JwtPrivateKey =
            builder.Configuration.GetValue<string>("JwtPrivateKey") ?? string.Empty;
    }

    public static void AddConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
    }

    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TechLibraryDbContext>
            (options => options.UseSqlite(ApiConfiguration.ConnectionString));
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();
    }

    public static void AddServices(this WebApplicationBuilder builder) 
    {
        #region httpcontext

        builder.Services.AddHttpContextAccessor();

        #endregion

        #region usecases

        builder.Services.AddTransient<DoLoginUseCase>();
        builder.Services.AddTransient<RegisterUserUseCase>();
        builder.Services.AddTransient<FilterBookUseCase>();
        builder.Services.AddTransient<RegisterBookCheckoutUseCase>();
        builder.Services.AddTransient<ReturnBookCheckoutUseCase>();

        #endregion

        #region loggeduser

        builder.Services.AddTransient<LoggedUserService>();

        #endregion
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication
            (options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer
            (options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = JwtTokenGenerator.SecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            }
            );
    }
}
