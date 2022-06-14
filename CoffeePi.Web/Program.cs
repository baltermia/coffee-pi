using CoffeePi.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") });
//if (builder.HostEnvironment.IsDevelopment())

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7256/api/") });

builder.Services.AddMudServices();



await builder.Build().RunAsync();
