using WebsiteLibrary.Models.Entites;
using WebsiteLibrary.Models.Interface;
using WebsiteLibrary.Models.Service;

namespace WebsiteLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<LibraryDatabaseContext>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IFineService, FineService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IUserService, UserService>();
            
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
        }
    }
}
