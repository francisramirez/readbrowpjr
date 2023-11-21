using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReadCrows.Infraestructure.Context;
using ReadCrows.Infraestructure.Interfaces;
using ReadCrows.Infraestructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregar dependencia del contexto //

builder.Services.AddDbContext<ReadCrowsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReadCrowsContext")));


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Para poder utilizar en diferentes aplicativos asi como Angular, React, Ect...
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Usuario API",
        Description = "Api para administrar los usuarios",
    }
    );
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
});

#region"Token Config"
var key = Encoding.ASCII.GetBytes(builder.Configuration["TokenInfo:SigningKey"]);

builder.Services.AddAuthentication(jb =>
{
    jb.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    jb.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie()
  .AddJwtBearer(jb =>
             {
                 jb.RequireHttpsMetadata = false;
                 jb.SaveToken = true;
                 jb.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                 };
             });


#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
