using Microsoft.EntityFrameworkCore;
using NewAuthCustomAccountTestEnv.Data;
using NewAuthCustomAccountTestEnv.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed((host) => true)
    .AllowAnyHeader()

    );
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admins", policy =>
    {
        policy.RequireRole("Admin");
    });
}
);
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AuthorizeFolder("/Admin/Users", "Admins");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<LeaderBoardNotifyHub>("/LeaderBoardNotifyHub");

app.Run();