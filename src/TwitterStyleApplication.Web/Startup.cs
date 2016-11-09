using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using TwitterStyleApplication.DAL;
using TwitterStyleApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TwitterStyleApplication.DAL.Configuration;
using TwitterStyleApplication.DAL.Contracts;
using TwitterStyleApplication.DAL.Contracts.Initializers;
using TwitterStyleApplication.DAL.Contracts.Repositories;
using TwitterStyleApplication.DAL.Repositories;
using TwitterStyleApplication.Services.Contracts;
using TwitterStyleApplication.Services.Implementation;
using TwitterStyleApplication.Web.Configuration;

namespace TwitterStyleApplication.Web
{
	public partial class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");
			var connectionStringConfig = builder.Build();

			//services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(
			//	connectionStringConfig.GetConnectionString("DefaultConnection")));

			services.AddDbContext<DataDbContext>(opt => opt.UseInMemoryDatabase());

			services.AddIdentity<ApplicationUser, IdentityRole>(pass =>
			{
				pass.Password.RequireDigit = false;
				pass.Password.RequiredLength = 6;
				pass.Password.RequireNonAlphanumeric = false;
				pass.Password.RequireUppercase = false;
				pass.Password.RequireLowercase = false;
			}).AddEntityFrameworkStores<DataDbContext>();


			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			});

			services.AddSwaggerGen(options =>
			{
				options.SingleApiVersion(new Info
				{
					Version = "v1",
					Title = "TwtitterStyleApp",
					Description = "API documentation",
					TermsOfService = "None"
				});
				options.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
				options.DescribeAllEnumsAsStrings();


			});

			// DI
			services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITweetsService, TweetsService>();
			services.AddScoped<ITweetRepository, TweetRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IServiceProvider services, IDatabaseInitializer initializer)
		{
			ConfigureAuth(app, services);
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseCors(builder =>
					// This will allow any request from any server. 
					builder
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin());

			AutomapperConfiguration.Load();
		

			app.UseDeveloperExceptionPage();
			app.UseMvc();

			app.UseSwagger((httpRequest, swaggerDoc) =>
			{
				swaggerDoc.Host = httpRequest.Host.Value;
			});

			app.UseSwaggerUi(baseRoute: "swagger", swaggerUrl: "/swagger/v1/swagger.json");
			app.UseMvcWithDefaultRoute();
			//// Recreate db's
			initializer.Seed().GetAwaiter().GetResult();
		}

		private string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
		{
			return Path.Combine(appEnvironment.ApplicationBasePath, "TwitterStyleApplication.Web.xml");
		}
	}
}
