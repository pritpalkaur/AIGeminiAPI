using Google.GenAI;
using Google.GenAI.Types;

namespace GeminiApi.Services;

public interface IGeminiService
{
    Task<string> ChatAsync(string userMessage);
}

public class GeminiService : IGeminiService
{
    private readonly Client _client;
    private const string ModelId = "gemini-2.0-flash";

    public GeminiService(IConfiguration config)
    {
        var apiKey = config["Gemini:ApiKey"]
            ?? throw new Exception("Gemini API Key missing");

        _client = new Client(apiKey: apiKey);
    }

    public async Task<string> ChatAsync(string userMessage)
    {
        var request = new List<Content>
        {
            new Content
            {
                Role = "user",
                Parts = new List<Part>
                {
                    new Part { Text = userMessage }
                }
            }
        };

        var response = await _client.Models.GenerateContentAsync(
            model: ModelId,
            contents: request
        );

        var text = response.Candidates?
            .FirstOrDefault()?
            .Content?
            .Parts?
            .FirstOrDefault()?
            .Text;

        return text ?? "No response from Gemini";
    }
}