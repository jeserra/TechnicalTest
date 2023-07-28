// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.Console;
using TechnicalTest.Data;

Console.WriteLine("Welcome to this demo!");

string connectionString = "Host=localhost;Database=Products;Username=postgres;Password=!qazxsw2";
string pathFile = "C:\\Users\\joser\\code\\TechnicalTest\\TechnicalTest.Console\\Resources\\TestExampleFile.csv";

var services = new ServiceCollection();

services.AddSingleton<ImportService>();
services.AddTransient<IProductsSourceService, ProductsService>();

//services.AddDbContext<ProductsContext>(options => options.UseNpgsql(connectionString));
services.AddDbContext<ProductsContext>();
ServiceProvider serviceProvider = services.BuildServiceProvider();


var dbContext = serviceProvider.GetRequiredService<ProductsContext>();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();


/*if (!dbContext.Database.EnsureCreated())
{
    Console.WriteLine("Database is not created");
    return;
}*/

/*var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

if (pendingMigrations.Any())
{
    Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
    Console.WriteLine("Applying pending migrations now");
    await dbContext.Database.MigrateAsync();
} */

//var lastAppliedMigration = (await dbContext.Database.GetAppliedMigrationsAsync()).Last();
//Console.WriteLine($"You're on schema version: {lastAppliedMigration}");


Console.WriteLine("Inserting Data!! ");
var importService = serviceProvider.GetRequiredService<ImportService>();
var totalProducts  = importService.ImportData(pathFile);

Console.WriteLine($"Total products: {totalProducts}");

Console.WriteLine("Finished import, please press any key to finish!! ");

Console.ReadKey();

