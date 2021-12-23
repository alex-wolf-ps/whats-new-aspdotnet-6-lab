using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WiredBrainCoffee.UI;
using WiredBrainCoffee.UI.Services;
using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffee.UI.Components;
using Microsoft.Extensions.Logging;
using System;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.RootComponents.RegisterForJavaScript<GlobalAlert>(identifier: "globalAlert");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAntiforgery(x => x.SuppressXFrameOptionsHeader = true);

var hostname = builder.Configuration["host"];
builder.Services.AddBlazoredModal();
builder.Services.AddBlazorise(options =>
{
    options.ChangeTextOnKeyPress = true;
})
  .AddBootstrapProviders()
  .AddFontAwesomeIcons();

builder.Services.AddHttpClient<IMenuService, MenuService>(client =>
    client.BaseAddress = new Uri(hostname));
builder.Services.AddHttpClient<IContactService, ContactService>(client =>
    client.BaseAddress = new Uri(hostname));
builder.Services.AddHttpClient<IOrderService, OrderService>(client =>
    client.BaseAddress = new Uri(hostname));

var host = builder.Build();

await host.RunAsync();
