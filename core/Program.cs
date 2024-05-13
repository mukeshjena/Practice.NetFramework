using CrudUsingCore.DbCtx;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataAccess>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs"));
    var connectionString = builder.Configuration.GetConnectionString("dbcs");
    var dBbuilder = new SqlConnectionStringBuilder(connectionString);
    dBbuilder.InitialCatalog = "MukeshDb"; // Dynamically change the catalog name
    options.UseSqlServer(dBbuilder.ConnectionString);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
