using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using storageUnitAPi;
using storageUnitAPi.Data;
using storageUnitAPi.Interfaces;
using storageUnitAPi.Repositories;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddScoped<ISpellsRepository, SpellsRepository>();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// if (args.Length == 1 && args[0].ToLower() == "seeddata")
SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DbInitializer>();
        service.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
