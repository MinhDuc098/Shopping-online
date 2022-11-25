using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Add 

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();




var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();


//Edit
//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.MapRazorPages();
app.Run();