using CityInfoApi.DbContexts;
using CityInfoApi.MailService;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CityInfoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //For this 2 packages are required install-package serilog.sinks.file and install-package serilog.sinks.console
            Log.Logger=new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/cityInfoApiLog.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .CreateLogger();


            var builder = WebApplication.CreateBuilder(args);

            //builder.Logging.ClearProviders(); //Clear the default logging providers which is given in the appsettings.json file
            //builder.Logging.AddConsole(); //Add console logging provider


            //Add serilog to the builder.Before adding this configure the serilog in the top of the file [line 11-16] also comment the default logging provider [lin 21,22].
            builder.Host.UseSerilog();

            // Add services to the container
            //builder.Services.AddControllers();
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; //If the client does not accept the response type, return 406 Not Acceptable.If will not accept the request of the client because the response type will not support the client.
            }).AddNewtonsoftJson()
               .AddXmlDataContractSerializerFormatters(); //Here we are specifying that we can send the reponse in xml format,so it accept the request if the requested format is xml.

            //Add some extra details to the response .So that the client can get more details about the response
            //builder.Services.AddProblemDetails(options =>
            //{

            //    options.CustomizeProblemDetails = ctx =>
            //    {
            //        ctx.ProblemDetails.Extensions.Add("additionalInfo", "Additional info example");
            //        ctx.ProblemDetails.Extensions.Add("server", Environment.MachineName);
            //    };
            //});




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //To get file type dynamically we use this class
            builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

            builder.Services.AddSingleton<CityDataSource>();

            ///Install install-package microsoft.entityframeworkcore.To work with MySqli install Microsoft.EntityFrameworkCore.Sqlite and install  microsoft.entityframeworkcore.Tools for Migration commands
            builder.Services.AddDbContext<CityInfoApiContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:connectString"]));

            #if DEBUG
            builder.Services.AddScoped<IMailService, LocalMailService>(); //This is for the local mail service             
            #else
            builder.Services.AddScoped<IMailService, CloudMailService>(); //This is for the cloud mail service
            #endif

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();


            //app.MapControllers();
            app.UseEndpoints(endPoints =>
            {
                endPoints.MapControllers();
            }
            );

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.Run();
        }
    }
}
