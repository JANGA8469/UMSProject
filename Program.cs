using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UMSProject.Data;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// Database Configuration
// ==========================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// ==========================================
// Identity Configuration
// ==========================================
builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        // Sign In
        options.SignIn.RequireConfirmedAccount = false;

        // Password
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;

        // Lockout
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

        // User
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()                        // Enable Roles
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ==========================================
// MVC + Razor Pages
// ==========================================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ==========================================
// Session
// ==========================================
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ==========================================
// Build App
// ==========================================
var app = builder.Build();

// ==========================================
// Configure HTTP Pipeline
// ==========================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

// ==========================================
// MVC Route
// ==========================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

// ==========================================
// Identity Razor Pages
// ==========================================
app.MapRazorPages();

app.Run();