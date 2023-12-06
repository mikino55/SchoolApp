using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Infrastructure.Configuration;
public class JwtOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string Secret { get; set; }

    public const string JwtOptionsName = "JwtOptions";
}

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public void Configure(JwtOptions options)
    {
        var section = this.configuration.GetSection(JwtOptions.JwtOptionsName);
        options.Issuer = section.GetValue<string>(nameof(JwtOptions.Issuer))!;
        options.Audience = section.GetValue<string>(nameof(JwtOptions.Audience))!;
        options.Secret = section.GetValue<string>(nameof(JwtOptions.Secret))!;
    }
}
