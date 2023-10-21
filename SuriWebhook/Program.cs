
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var app = builder.Build();

var serverApiKey = builder.Configuration["AppSettings:serverApiKey"];

var connectionString = builder.Configuration["AppSettings:ConnectionString"];

app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        var apiKey = context.Request.Headers["Authorization"];
        if (apiKey == serverApiKey)
        {
            await next();
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
    }
    else
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return;
    }
});

app.MapGet("/webhook", () => "cb6262!");

app.MapPost("/webhook", async context => {

    var requestBody = await context.Request.ReadFromJsonAsync<WebhookPayload>();
    
    await context.Response.WriteAsync(requestBody);
});

app.Run();

public record WebhookPayload(String Id, String Type, TimestampAttribute Timestamp, JsonElement Payload);

