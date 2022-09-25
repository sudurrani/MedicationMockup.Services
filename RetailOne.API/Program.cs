//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using ARPlace.Application.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using MedicationMockup.API.Dependencies;
using MedicationMockup.API.Filters;
using MedicationMockup.API.Middlewares;
using MedicationMockup.Application.Mapper;
using MedicationMockup.Application.Shared.Common.Dtos;
using MedicationMockup.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger
//builder.Services.AddSwaggerGen();
//Commented above code and used below to use Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Medication Mock-up",
        Version = "v1",
        //Description = "<h3>Web API's for managing Retail-one items</h3>",
        TermsOfService = new Uri("http://www.arplace.io/"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("http://www.arplace.io/")
        },
        License = new OpenApiLicense
        {
            Name = "Licensing",
            Url = new Uri("http://www.arplace.io/")
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
});

//JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    //// Other configs...
    //o.Events = new JwtBearerEvents
    //{
    //    OnChallenge = async context =>
    //    {
    //        var _responseOutputDto = new ARPlace.Application.Shared.Common.Dtos.ResponseOutputDto();
    //        // Call this to skip the default logic and avoid using the default response
    //        context.HandleResponse();

    //        // Write to the response in any way you wish
    //        context.Response.StatusCode = 502;
    //        context.Response.Headers.Append("my-custom-header-y", "custom-value-y");
    //        await context.Response.WriteAsync("You are not authorized! (or some other custom message)");
    //        await context.HttpContext.Response.WriteAsync(
    //                JsonConvert.SerializeObject(_responseOutputDto.Status401Unauthorized()));
    //    }
    //};
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});
//JWT End


#endregion

#region AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new EntityToOutputDtoMapper());
    cfg.AddProfile(new InputDtoToEntityMapper());
    cfg.ValidateInlineMaps = false;
});

var mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);
#endregion

#region Register Services
//Registring Services
builder.Services.AddDbContext<MedicationMockupDbContext>(options =>
options.UseSqlServer(builder.Configuration["AppSettings:ConnectionString"])
);
builder.Services.RegisterServices();
// End Registering Services
#endregion


// Add services to the container.
builder.Services.AddControllers(options =>
{
    //Add Your Filter
    options.Filters.Add<CustomValidationFilterAttribute>();
}).ConfigureApiBehaviorOptions(options =>
{
    //Disable The Default
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    //app.UseExceptionHandler(exceptionHandlerApp =>
    //{
    //    exceptionHandlerApp.Run(async context =>
    //    {
    //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    //        // using static System.Net.Mime.MediaTypeNames;
    //        context.Response.ContentType = Text.Plain;

    //        await context.Response.WriteAsync("An exception was thrown.");

    //        var exceptionHandlerPathFeature =
    //            context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

    //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
    //        {
    //            await context.Response.WriteAsync(" The file was not found.");
    //        }

    //        if (exceptionHandlerPathFeature?.Path == "/")
    //        {
    //            await context.Response.WriteAsync(" Page: Home.");
    //        }
    //    });
    //});
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My service");
    //    if (app.Environment.IsProduction())
    //    {
    //        c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
    //    }
    //});

}
//Added this code into Startup -> Configure method to allow request from any site
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<MedicationMockupDbContext>();
    dataContext.Database.Migrate();
}

// put middleware before authentication
//app.UseMiddleware<ResponseFormatterMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Unauthorized (401) MiddleWare - Customize Unauthorized-401 request response
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 401)//(int)HttpStatusCode.Unauthorized) // 401
    {
        var _responseOutputDto = new ResponseOutputDto();
        _responseOutputDto.Status401Unauthorized();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(_responseOutputDto.Status401Unauthorized()));
    }
});
app.UseAuthentication(); // This need to be added	JWT
app.UseAuthorization();
app.MapControllers();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.Run();


