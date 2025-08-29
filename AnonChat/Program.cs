using AnonChat.Data;
using AnonChat.Services.Implementations;
using AnonChat.Services.Implementations.Pattern;
using AnonChat.Services.Interface;
using AnonChat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;               
    options.Cookie.IsEssential = true;          
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<CleanService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AnonChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AnonChat")));

builder.Services.AddScoped<ChatStatusService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserFactory, NormalUserFactory>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRouting();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Match}/{action=Index}/{id?}");

app.Run();
