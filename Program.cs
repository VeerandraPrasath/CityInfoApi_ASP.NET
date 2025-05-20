
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            //builder.Services.AddControllers();

            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true; //If the client does not accept the response type, return 406 Not Acceptable.If will not accept the request of the client because the response type will not support the client.
            }).AddXmlDataContractSerializerFormatters(); //Here we are specifying that we can send the reponse in xml format,so it accept the request if the requested format is xml.

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
