using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDBConnectionStrings") ?? throw new InvalidOperationException("Connection string 'BethanyPieShopDbContextConnection' not found.");

builder.Services.AddScoped<ICatogeryRepository,CategoryRepository>();

builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));

builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddRazorPages();
 
builder.Services.AddDbContext<BethanyPieShopDbContext>(
    options=>options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDBConnectionStrings"]));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanyPieShopDbContext>();

//builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//app.UseHttpsRedirection();

app.MapDefaultControllerRoute();

//app.MapControllerRoute(
//   name:"default",
//   pattern:"{controller=Home}/{action=Index}/{id:int?}");

app.MapRazorPages();

DbInitializer.Seed(app);

app.Run();
 