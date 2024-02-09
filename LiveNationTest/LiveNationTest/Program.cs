
using Microsoft.EntityFrameworkCore;
using NumberConverter.Converting;
using NumberConverter.Converting.Interfaces;
using NumberConverter.Rules;
using NumberConverter.Rules.Interfaces;
using RuleRepository;

namespace LiveNationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<RuleContext>(opt =>
                opt.UseInMemoryDatabase("Rules"));

            builder.Services.AddScoped<IRuleStore, RuleStore>();
            builder.Services.AddScoped<IConverterRuleStore, RuleStore>();

            builder.Services.AddTransient<ConverterService>();
            builder.Services.AddTransient<RuleService>();

            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                using (var scope = app.Services.CreateScope())
                {
                    scope.ServiceProvider.GetRequiredService<RuleContext>().Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}