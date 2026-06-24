using BLL.Services;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
//using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("DefaultConnection")!;

// -- DAL Repositories ----------------------------------------------
builder.Services.AddScoped<IClassRepository>(_ => new ClassRepository(cs));
builder.Services.AddScoped<IStudentRepository>(_ => new StudentRepository(cs));
builder.Services.AddScoped<ISubjectRepository>(_ => new SubjectRepository(cs));
builder.Services.AddScoped<IResultRepository>(_ => new ResultRepository(cs));
builder.Services.AddScoped<IUserRepository>(_ => new UserRepository(cs));
builder.Services.AddScoped<IDashboardRepository>(_ => new DashboardRepository(cs));

// -- BLL Services ----------------------------------------------
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IResultService, ResultService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// -- JWT Authentication ----------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// -- Swagger with JWT Bearer support ------------------------------
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Student Management & Result Portal API",
//        Version = "v1",
//        //Description = "ASP.NET Core 10 Web API — 3-Tier Architecture"
//    });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        //Description = "Enter your JWT token. Example: Bearer eyJhbGci..."
//    });
////    c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
////    new OpenApiSecurityScheme {
////        Reference = new OpenApiReference
////        {
////            Type = ReferenceType.SecurityScheme,
////            Id = "Bearer"
////        }
////    },
////    Array.Empty<string>()
////}});
//});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Management API",
        Version = "v1"
    });

    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("bearer", document)] = []
        });
});

// ---------------- CORS --------------------------------
builder.Services.AddCors(opt =>
    opt.AddPolicy("AllowReact", p =>
        p.WithOrigins("http://localhost:3000", "http://localhost:5173")
         .AllowAnyMethod()
         .AllowAnyHeader()));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Portal API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowReact");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("Admin@123", 11));
app.Run();