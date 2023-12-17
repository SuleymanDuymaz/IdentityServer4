
using IdentityServer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
// Add services to the container.
builder.Services.AddControllersWithViews();
//use default connection for entityframework

////custom ýdentity implementation
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//  .AddEntityFrameworkStores<Identity.Data.ApplicationDbContext>().AddDefaultTokenProviders();




builder.Services.AddRazorPages();
builder.Services.AddIdentityServer(options =>
{
})
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddInMemoryClients(Config.Clients)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddDeveloperSigningCredential();


builder.Services.AddMvc().AddMvcOptions(o => o.EnableEndpointRouting = true);






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "img-src 'self' data:;");
    await next();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();





