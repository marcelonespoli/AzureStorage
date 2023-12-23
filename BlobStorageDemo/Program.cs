using Azure.Data.Tables;
using BlobStorageDemo.Services;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);
var storageConnectionString = builder.Configuration["AzureStorage:ConnectionString"];
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAzureClients(azBuilder =>
{
    azBuilder.AddTableServiceClient(storageConnectionString);
});
builder.Services.AddAzureClients(azBuilder =>
{
    azBuilder.AddClient<TableClient, TableClientOptions>((_, _, _) =>
    {
        return new TableClient(storageConnectionString,
                builder.Configuration["AzureStorage:TableStorage"]);
    });
});

builder.Services.AddScoped<ITableStorageService, TableStorageService>();


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
