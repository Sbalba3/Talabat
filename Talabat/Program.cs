
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.Core.IRepository;
using Talabat.Core.Models;
using Talabat.Core.Models.Identity;
using Talabat.Errors;
using Talabat.Extensions;
using Talabat.Helpers;
using Talabat.MiddleWares;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Services Container
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));

            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));

            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var connection = builder.Configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(connection);

            });

            IdentityServicesExtention.AddIdentityServices(builder.Services);
            builder.Services.AddApplicationServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();


            #endregion
           var app = builder.Build();
            using var scope=app.Services.CreateScope();//create scope explicity
            var service=scope.ServiceProvider;
            var loggerfactory=service.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = service.GetRequiredService<StoreContext>(); //ask clr to creat obj from dbcontext explicity
                await dbContext.Database.MigrateAsync();// update-database
                await  StoreContextSeed.SeedAsync(dbContext);
                var identityDbContext= service.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync();
                var userManger=service.GetRequiredService<UserManager<AppUser> >();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex,ex.Message);

            }


            // Configure the HTTP request pipeline.
            #region kestrel
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}"); 
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();


            #endregion
            app.Run();
        }
    }
}