using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin",
		builder=>builder.WithOrigins("https://localhost:44366"));
});
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = tokenOptions.Issuer,
		ValidAudience = tokenOptions.Audience,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)



	};
});
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserManager>();
builder.Services.AddScoped<ITokenHelper,JwtHelper >();

var connStr = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<ProjectGContext>(options =>
{
	options.UseSqlServer(connStr);
});


//                           /\
//________________________  //\\
//     UP IS Services    |   ||
//------------------------   ||
//                           --


//                           --
//________________________   ||
//  Down IS MiddleWares  |   ||
//------------------------  \\//
//    

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();
app.UseCors(builder => builder.WithOrigins("https://localhost:44366").AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Ui}/{id?}");

app.UseMiddleware<ValidateTokenMiddleware>();
app.UseMiddleware<JwtCurrentUserMiddleware>();


app.Run();
