using Microsoft.EntityFrameworkCore;

namespace HRPortal.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();
            //builder.Services.AddDbContext<HRPortalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HRPortalContext")));
            // for postgresql
            //builder.Services.AddDbContext<HRPortalContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("HRPortalContext")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddCors(options => {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => {
                        builder.WithOrigins("http://localhost:4200") // Replace with your frontend application's URL
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); // You might need this if your WebSocket server requires credentials
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}