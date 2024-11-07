using System.Text.Json;
using DTO;

namespace BlazorApp.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<CreateUpdatePostDto> CreatePostAsync(CreateUpdatePostDto request)
    {
        HttpResponseMessage httpResponseMessage = await client.PostAsJsonAsync("/Posts", request);
        string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Server error: {httpResponseMessage.StatusCode}. Content: {responseContent}");
        }
        return JsonSerializer.Deserialize<CreateUpdatePostDto>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<List<PostDto>> GetallPostsAsync()
    {
        HttpResponseMessage httpResponseMessage = await client.GetAsync("/Posts");

        // Check if the response was successful
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var posts = await httpResponseMessage.Content.ReadFromJsonAsync<List<PostDto>>();
            return posts;
        }
        throw new Exception($"Failed to retrieve posts: {httpResponseMessage.ReasonPhrase}");
    }
}