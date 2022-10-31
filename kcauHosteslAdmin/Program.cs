using DAL_CRUD.Data;
using DAL_CRUD.Models;
using kcauHosteslAdmin.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRequestsService<Armenity>, RequestsService<Armenity>>((s) => new RequestsService<Armenity>("Amenities")); //Booking
builder.Services.AddTransient<IRequestsService<Book>, RequestsService<Book>>((s) => new RequestsService<Book>("Booking")); //
builder.Services.AddTransient<IRequestsService<User>, RequestsService<User>>((s) => new RequestsService<User>("Users")); //
builder.Services.AddTransient<IRequestsService<RentAlternative>, RequestsService<RentAlternative>>((s) => new RequestsService<RentAlternative>("RentAlternatives")); //
builder.Services.AddTransient<IRequestsService<Hostel>, RequestsService<Hostel>>((s) => new RequestsService<Hostel>("Hostels")); //

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
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
