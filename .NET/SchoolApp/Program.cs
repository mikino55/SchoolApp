using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolApp;
using SchoolApp.Client.Pages;
using SchoolApp.Components;
using SchoolApp.Components.Account;
using SchoolApp.Data;
using SchoolApp.Data.Access;
using SchoolApp.Data.Access.Providers;
using SchoolApp.EndpointMapping;
using SchoolApp.Extensions;
using SchoolApp.Infrastructure.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

services.AddCascadingAuthenticationState();
services.AddScoped<IdentityUserAccessor>();
services.AddScoped<IdentityRedirectManager>();
services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
services.AddTransient<IUserRegistrationProvider, UserRegistrationProvider>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddDefaultIdentity<User>((o =>
{
    o.SignIn.RequireConfirmedAccount = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireDigit = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.User.RequireUniqueEmail = false;
}))
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//services.AddIdentityApiEndpoints<User>((o =>
//{
//    o.SignIn.RequireConfirmedAccount = false;
//    o.Password.RequireNonAlphanumeric = false;
//    o.Password.RequireDigit = false;
//    o.Password.RequireUppercase = false;
//    o.Password.RequireLowercase = false;
//}))
//    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddSingleton<IEmailSender<User>, IdentityNoOpEmailSender>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR((o) => o.RegisterServicesFromAssembly(typeof(Program).Assembly));
services.AddCors();


services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.JwtOptionsName));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
{
    // find the way how to inject JwtOptions into this configuration, last attemp did not work, 
    var section = builder.Configuration.GetSection(JwtOptions.JwtOptionsName);
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = section.GetValue<string>(nameof(JwtOptions.Issuer)),
        ValidAudience = section.GetValue<string>(nameof(JwtOptions.Audience)),
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(section.GetValue<string>(nameof(JwtOptions.Key))!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseIdentity();
app.UseAuthorization();

//app.MapGroup("/account").MapIdentityApi<User>();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// https://jasonwatmore.com/post/2020/05/20/aspnet-core-api-allow-cors-requests-from-any-origin-and-with-credentials
// global cors policy
var allowedHosts = new []{ "^(www.)?localhost" };
app.UseCors(x => x
     .SetIsOriginAllowed((url) => CorsHelper.IsCorsOriginAllowed(url, allowedHosts))
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod());


// Add additional endpoints required by the Identity /Account Razor components.
//app.MapAdditionalIdentityEndpoints();
app.MapStudentEndpoints();
app.MapUserEndpoints();
app.MapTecherEndpoints();


//await SeedData.Seed(app.Services);

app.Run();
