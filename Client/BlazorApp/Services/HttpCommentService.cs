using System.Text.Json;
using DTO;

namespace BlazorApp.Services;

public class HttpCommentService : ICommentService
{
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<CreateUpdateCommentDto> createCommentasync(CreateUpdateCommentDto comment)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("id/comments", comment);
        string responseString = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Server error: {response.StatusCode}. Content: {responseString}");
        }
        return JsonSerializer.Deserialize<CreateUpdateCommentDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}