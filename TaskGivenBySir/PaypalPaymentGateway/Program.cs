using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayPalGateway.DbCtx;
using PayPalGateway.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddDbContext<PaymentDbContext>(options =>
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
