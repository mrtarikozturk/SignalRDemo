using SignalRDemo.Components;
using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomSignalR(); // SignalR hizmetlerini ekliyoruz. Bu, SignalR hub'larını ve diğer gerekli bileşenleri yapılandırır.


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SignalRDemo.Client._Imports).Assembly);
app.UseCustomSignalR(); // SignalR yapılandırmasını uyguluyoruz. Bu, SignalR hub'larını ve middleware'i ekler.
app.Run();
