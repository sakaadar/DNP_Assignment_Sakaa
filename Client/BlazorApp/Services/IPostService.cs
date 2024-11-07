using DTO;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<CreateUpdatePostDto> CreatePostAsync(
        CreateUpdatePostDto request);
    public Task<List<PostDto>> GetallPostsAsync();
}