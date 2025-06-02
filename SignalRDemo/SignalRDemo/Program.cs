using Microsoft.AspNetCore.ResponseCompression;
using SignalRDemo.Components;
using SignalRDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR(); // SignalR servisini ekliyoruz. Bu servis SignalR hub'larini kullanabilmemizi sagliyor.  

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
}); // Response compression servisini ekliyoruz. Bu servis HTTP response'larini sikistiriyor. Bu sayede HTTP response'lari daha kucuk boyutlarda gonderilebiliyor. Bu servis HTTP response'larini sikistirirken hangi mime type'larin sikistirilip sikistirilmayacagini belirliyor. Burada application/octet-stream mime type'i ekleniyor. Bu mime type'i ekleyerek SignalR hub'larinin HTTP response'larini sikistiriyoruz. Bu sayede SignalR hub'larinin HTTP response boyutlari daha kucuk oluyor ve daha hizli gonderilebiliyor. 


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
app.MapHub<MyHub>("/chathub"); // SignalR hub'u burada tanimlaniyor. Client tarafindan bu hub'a baglanilabilir. Bu hub'a baglanmak icin client tarafinda bir SignalR client'i olusturulmalidir. Bu client hub'a baglanabilir ve hub'dan mesaj alabilir. Hub'a baglanmak icin SignalR client'inin StartAsync metodu kullanilabilir. Bu metot hub'a baglanir ve hub'dan mesaj alir. Hub'dan mesaj almak icin SignalR client'inin On metodu kullanilabilir. Bu metot hub'dan gelen mesajlari dinler ve mesajlari alir.
app.Run();
