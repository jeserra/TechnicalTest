// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.CommandLine;


using TechnicalTest.Console;
using TechnicalTest.Data;
using System.Configuration;

Console.WriteLine("Welcome to this demo!");


IConfiguration Configuration = new ConfigurationBuilder()
    .SetBasePath(System.AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)     
    .Build();

var connectionString = Configuration.GetConnectionString("PostgreSQL");
var pathFile = Configuration["SourceFile"];

var services = new ServiceCollection();

services.AddSingleton<ImportService>();
services.AddTransient<IProductsSourceService, ProductsService>();

services.AddDbContext<ProductsContext>(options => options.UseNpgsql(connectionString));
services.AddDbContext<ProductsContext>();
ServiceProvider serviceProvider = services.BuildServiceProvider();


var dbContext = serviceProvider.GetRequiredService<ProductsContext>();

dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

// Code to apply migrations, from here, there is not inserting migrations in the table, but it does running from ef.
// Pending to research the problem.
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

