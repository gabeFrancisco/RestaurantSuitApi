using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs.Mappings;
using RecantosSystem.Api.Filters;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Services;
using RecantosSystem.Api.Services.Logging;
using RecantosSystem.Api.Services.Security;

namespace RecantosSystem.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();

			// AppDbContext setup
			string mySqlConnection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<AppDbContext>(options =>
				options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

			// AutoMapper config
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappingConfig.CreateMapper();

			// Dependency injection 
			services.AddHttpContextAccessor();
			services.AddSingleton(mapper);
			services.AddSingleton<IUserAccessor, HttpUserAccessor>();
			services.AddLogging();

			services.AddScoped<LogService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<TokenService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<ITableService, TableService>();

			// Bellow is the Jwt Bearer config.
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidAudience = Configuration["TokenConfiguration:Audience"],
						ValidIssuer = Configuration["TokenConfiguration:Issuer"],
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
						)
					});

			services.AddControllers(options => 
				{
					options.Filters.Add<ExceptionsAttribute>();
				}
			);

			// Swagger config
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "RecantosSystem.Api", Version = "v1" });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				c.IncludeXmlComments(xmlPath);

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement(){
					{
						new OpenApiSecurityScheme{
							Reference = new OpenApiReference{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecantosSystem.Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
