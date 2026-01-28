using Microsoft.AspNetCore.Mvc;
using GeminiApi.Services;

[ApiController]
[Route("api/[controller]")]
public class AiController(IGeminiService geminiService) : ControllerBase
{
    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] PromptRequest request)
    {
        if (string.IsNullOrEmpty(request.Message)) return BadRequest();

        var reply = await geminiService.ChatAsync(request.Message);
        return Ok(new { content = reply });
    }
}

public record PromptRequest(string Message);