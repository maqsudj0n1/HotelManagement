using Application;
using HotelManagement1.Services;
using Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        // Add services to the container.

        //builder.Services.AddRateLimiter(rateLimiterOptions =>
        //  {
        //      rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //      rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
        //      {
        //          options.PermitLimit = 5;
        //          options.Window = TimeSpan.FromSeconds(30);
        //          options.SegmentsPerWindow = 2;
        //          options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        //          options.QueueLimit = 0;
        //      });
        //  });

        //builder.Services.AddRateLimiter(rateLimiterOptions =>
        //{
        //    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        //    rateLimiterOptions.AddTokenBucketLimiter("token", options =>
        //    {
        //        options.TokenLimit = 10;
        //        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        //        options.QueueLimit = 0;
        //        options.ReplenishmentPeriod = TimeSpan.FromSeconds(20);
        //        options.TokensPerPeriod = 5;
        //        options.AutoReplenishment = true;
        //    });
        //});

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

       builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddLazyCache();
        builder.Services.AddWebUIServices();
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            c.DisplayRequestDuration()
            );
        }
        app.UseRateLimiter();


        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}");




      //  app.MapControllers();

        app.Run();
    }
}