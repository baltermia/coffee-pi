using CoffeePi.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") });
//if (builder.HostEnvironment.IsDevelopment())

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://coffeepi/api/") });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
