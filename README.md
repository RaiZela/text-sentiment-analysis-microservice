# Text Sentiment Analysis Microservice

A simple backend service in C# that performs **sentiment analysis** on multiple comments using **Azure Text Analytics**.

---

## 🔹 Features

- Accepts a JSON object with multiple comments.
- Analyzes sentiment for each comment:
  - **Positive**
  - **Neutral**
  - **Negative**
- Returns sentiment along with confidence scores per comment.
- Backend-only service (no frontend required).

---

## 🔹 Sample Input

```json
{
  "comments": [
    { "id": "1", "user": "Alice", "date": "2025-08-25", "text": "I love this product, it works really well!" },
    { "id": "2", "user": "Bob", "date": "2025-08-24", "text": "It's okay, not too bad but not amazing either." },
    { "id": "3", "user": "Charlie", "date": "2025-08-23", "text": "Terrible experience, I will never use this again!" }
  ]
}
```

## 🔹 Sample Output

```json
[
  {
    "Id": "1",
    "User": "Alice",
    "Date": "2025-08-25",
    "Text": "I love this product, it works really well!",
    "Sentiment": "Positive",
    "Scores": { "Positive": 0.98, "Neutral": 0.01, "Negative": 0.01 }
  },
  {
    "Id": "2",
    "User": "Bob",
    "Date": "2025-08-24",
    "Text": "It's okay, not too bad but not amazing either.",
    "Sentiment": "Neutral",
    "Scores": { "Positive": 0.30, "Neutral": 0.60, "Negative": 0.10 }
  },
  {
    "Id": "3",
    "User": "Charlie",
    "Date": "2025-08-23",
    "Text": "Terrible experience, I will never use this again!",
    "Sentiment": "Negative",
    "Scores": { "Positive": 0.01, "Neutral": 0.02, "Negative": 0.97 }
  }
]

```

## 🔹 Getting Started
1. Create Azure Text Analytics Resource

Go to the Azure Portal
.

Click Create a resource → search for Language / Azure AI Language.

Fill in the required details (Subscription, Resource Group, Name, Region, Pricing Tier).

Click Review + Create → then Create.

2. Get Endpoint and API Key

Go to your resource → Keys and Endpoint.

Copy Key1 and Endpoint URL.

3. Configure in Your Project

Add the values in appsettings.json:

```json
{
  "AzureTextAnalytics": {
    "Endpoint": "https://<your-resource-name>.cognitiveservices.azure.com/",
    "ApiKey": "<your-key>"
  }
}
```

##🔹 Using Dependency Injection

Register TextAnalyticsClient in Program.cs:

```csharp
using Azure;
using Azure.AI.TextAnalytics;

var builder = WebApplication.CreateBuilder(args);

string endpoint = builder.Configuration["AzureTextAnalytics:Endpoint"];
string apiKey = builder.Configuration["AzureTextAnalytics:ApiKey"];

builder.Services.AddSingleton(new TextAnalyticsClient(
    new Uri(endpoint),
    new AzureKeyCredential(apiKey)
));

var app = builder.Build();
app.Run();
```

## 🔹 Notes

This is a backend-only service.

No frontend/UI is included at this stage.

Optional improvements:

Aggregate sentiment summary.

Filtering by user or date.

Add unit tests or logging.
