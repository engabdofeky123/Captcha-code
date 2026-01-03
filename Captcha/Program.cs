
namespace Captcha
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(

                options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                    options.Cookie.HttpOnly = false; // Make the session cookie HTTP only or not
                    options.Cookie.IsEssential = true; // Make the session cookie essential
                }

                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllers();

            app.Run();
        }
    }
}
