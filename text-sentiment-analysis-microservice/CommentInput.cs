namespace text_sentiment_analysis_microservice;

public class CommentInput
{
    public string Id { get; set; }
    public string User { get; set; }
    public DateTime Date { get; set; }
    public string Text { get; set; }
}
