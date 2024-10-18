using Data_query.Models;
using Data_query.Data; // Thêm namespace cho SeedData
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace Data_query
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer(); // Giữ lại dòng này
            builder.Services.AddSwaggerGen(c => // Giữ lại block này
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); // Sử dụng OpenApiInfo trực tiếp
            });

            // Add context
            var connectionString = builder.Configuration.GetConnectionString("ConnectionDefault"); // Ensure this is correct
            var mysqlVersion = Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql");
            
            builder.Services.AddDbContext<TableQueryContext>(options =>
            {
                options.UseMySql(connectionString, mysqlVersion); // Ensure connection string matches appsettings.json
            });
 
            var app = builder.Build();

            // Initialize the database with seed data
            using (var scope = app.Services.CreateScope()) // Tạo scope để truy cập vào DbContext
            {
                var context = scope.ServiceProvider.GetRequiredService<TableQueryContext>();
                SeedData.Initialize(context); // Gọi phương thức Initialize để thêm dữ liệu khởi tạo
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(); // Giữ lại dòng này

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => // Giữ lại block này
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger"; // Set the route prefix for the Swagger UI
            });

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
