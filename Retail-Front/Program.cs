using Microsoft.Extensions.DependencyInjection;
using Retail_Api.Models;
using Retail_Front.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:xxxx") // Replace with your MVC application's URL
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddScoped<ProductSerice>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<Stocks_F_Service>();

var app = builder.Build();
//app.UseMvc();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
