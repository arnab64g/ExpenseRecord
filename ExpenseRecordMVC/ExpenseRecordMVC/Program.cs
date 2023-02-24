using DatabaseLayer;
using DatabaseLayer.Interface;
using ExpenseRecordMVC.Data;
using ExpenseService;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserData")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();    
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IExpenseRepository, ExpenseRepository>();
builder.Services.AddTransient<IExpenseService, ExpenseService.ExpenseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Expense/ExpenseList");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expense}/{action=ExpenseList}/{id?}");

app.Run();
