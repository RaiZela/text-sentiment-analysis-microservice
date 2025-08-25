using Azure.AI.TextAnalytics;

namespace text_sentiment_analysis_microservice;

public class SentimentService
{
    private readonly TextAnalyticsClient _textAnalyticsClient;

    public SentimentService(TextAnalyticsClient textAnalyticsClient)
    {
        _textAnalyticsClient = textAnalyticsClient;
    }

    public async Task<List<object>> AnalyzeSentimentBatchAsync(CommentRequest request)
    {
        var documents = request.Comments.Select(c => c.Text).ToList();

        var response = await _textAnalyticsClient.AnalyzeSentimentBatchAsync(documents);

        var results = new List<object>();

        for (int i = 0; i < response.Value.Count; i++)
        {
            var sentimentResult = response.Value[i];
            var originalComment = request.Comments[i];

            results.Add(new
            {
                Id = originalComment.Id,
                User = originalComment.User,
                Date = originalComment.Date,
                Text = originalComment.Text,
                Sentiment = sentimentResult.DocumentSentiment.Sentiment,
                Scores = new
                {
                    Positive = sentimentResult.DocumentSentiment.ConfidenceScores.Positive,
                    Neutral = sentimentResult.DocumentSentiment.ConfidenceScores.Neutral,
                    Negative = sentimentResult.DocumentSentiment.ConfidenceScores.Negative
                }
            });
        }

        return results;
    }

}
