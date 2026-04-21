using AspnetCoreMvcFull.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AspnetCoreMvcFull.Repositories;
using AspnetCoreMvcFull.Repository;
using AspnetCoreMvcFull.Service;
using AspnetCoreMvcFull.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductLTRepository, ProductLTRepository>();
builder.Services.AddScoped<IProductLTService, ProductLTService>();
builder.Services.AddScoped<IProductCvRepository, ProductCvRepository>();
builder.Services.AddScoped<IProductCvService, ProductCvService>();
builder.Services.AddScoped<IProductCvCTLRepository, ProductCvCTLRepository>();
builder.Services.AddScoped<IProductCvCTLService, ProductCvCTLService>();
builder.Services.AddScoped<IProductCvGCRepository, ProductCvGCRepository>();
builder.Services.AddScoped<IProductCvGCService, ProductCvGCService>();
builder.Services.AddScoped<IProductCSDRepository, ProductCSDRepository>();
builder.Services.AddScoped<IProductCSDService, ProductCSDService>();
builder.Services.AddScoped<IProductCSCTLRepository, ProductCSCTLRepository>();
builder.Services.AddScoped<IProductCSCTLService, ProductCSCTLService>();
builder.Services.AddScoped<IGangCauCTLRepository, GangCauCTLRepository>();
builder.Services.AddScoped<IGangCauCTLService, GangCauCTLService>();
builder.Services.AddScoped<ILuuHoaCTLRepository, LuuHoaCTLRepository>();
builder.Services.AddScoped<ILuuHoaCTLSevice, LuuHoaCTLSevice>();
builder.Services.AddScoped<IDongHangService, DongHangService>();
builder.Services.AddScoped<IDonghangRepository, DongHangRepository>();
builder.Services.AddScoped<IXuathangkhoRepository, XuathangkhoRepository>();
builder.Services.AddScoped<IXuathangSevice, XuathangSevice>();
builder.Services.AddScoped<IProductLTCTLService, ProductLTCTLService>();
builder.Services.AddScoped<IProductLTCTLRepository, ProductLTCTLRepository>();
builder.Services.AddScoped<IQuyCachCSCTLRepository, QuyCachCSCTLRepository>();
builder.Services.AddScoped<IQuyCachCSCTLService, QuyCachCSCTLService>();
builder.Services.AddScoped<ILuuHoaMHEService, LuuHoaMHEService>();
builder.Services.AddScoped<ILuuHoaMHERepository, LuuHoaMHERepository>();
builder.Services.AddScoped<IBangTaiRepository, BangTaiRepository>();
builder.Services.AddScoped<IBangTaiService, BangTaiService>();
builder.Services.AddScoped<ITTBangTaiRepository, TTBangTaiRepository>();
builder.Services.AddScoped<ITTBangTaiService, TTBangTaiService>();
builder.Services.AddScoped<IMSService, MSService>();
builder.Services.AddScoped<IMSRepository, MSRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboards}/{action=Index}/{id?}");

app.Run();
