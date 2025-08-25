using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.Mvc;
using text_sentiment_analysis_microservice;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new TextAnalyticsClient(
    new Uri(builder.Configuration["AzureTextAnalytics:Endpoint"]!),
    new AzureKeyCredential(builder.Configuration["AzureTextAnalytics:ApiKey"]!)
    ));

builder.Services.AddScoped<SentimentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPut("/sentiment", async ([FromBody] CommentRequest text, SentimentService sentimentService) =>
{
    var sentiment = await sentimentService.AnalyzeSentimentBatchAsync(text);
    return Results.Ok(new { Sentiment = sentiment });
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
