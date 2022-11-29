using CincyPay;
using CincyPay.Pages;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/CincyPayJobs/v2", async () =>
{
    SearchPageModel index = new SearchPageModel();
    string response = "";

    ApiResponse apiResponse = await index.GetApiResponseAsync();
    response = JsonConvert.SerializeObject(apiResponse, Formatting.Indented, new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore
    });

    return response;



})
.WithName("CincyPayJobs");

app.Run();
