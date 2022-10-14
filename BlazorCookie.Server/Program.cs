using BlazorCookie.Server.Data;
using BlazorCookie.Server.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

#region Authentication

// Add authentication services to the container
// This is used for when we want to protect our internal APIs with an auth state by using the cookies method
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(); // Passing the authentication scheme used for this app, "Cookies" or Built in constant

builder.Services.AddScoped<TokenProvider>(); // This will be used in the host file where we request the Xsrf token and pass it to the token provider
#endregion

// We register the IHttpClientFactory
builder.Services.AddHttpClient("dummyAuth", client =>
{
    client.BaseAddress = new Uri("https://localhost:7261/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

#region Authentication Middleware

app.UseAuthentication();
app.UseAuthorization();

#endregion

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers(); // this middleware needs to be added if we want to work with controllers/apis

app.Run();
