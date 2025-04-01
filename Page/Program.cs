using Assigment01_NguyenVietHoang_SE161371; // This line is causing the error
using Microsoft.AspNetCore.SignalR;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<INewArticleRepo, NewArticleRepo>();
builder.Services.AddScoped<ITagRepo, TagRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddSignalR();

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
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<SignalR>("/signalR");
app.MapGet("/test-signalr", async (IHubContext<SignalR> hubContext) =>
{
    await hubContext.Clients.All.SendAsync("TestEvent", "Hello from the server!");
    return Results.Ok("SignalR message sent.");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    // Redirect mặc định về trang Login
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/Login");
    });
});

app.Run();
