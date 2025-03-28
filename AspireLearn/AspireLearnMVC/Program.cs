using AspireLearnMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services with views
builder.Services.AddControllersWithViews();

// Enable session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register services for Web API communication
var apiBaseUrl = "https://localhost:7285/api/"; // ✅ Update this with your actual API URL
builder.Services.AddHttpClient<CourseService>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<AuthService>(client => client.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddHttpClient<ReviewService>(client => client.BaseAddress = new Uri(apiBaseUrl));

// Enable access to HttpContext (needed for accessing JWT token in session)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enable HTTP Strict Transport Security
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ✅ Enable session handling
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
